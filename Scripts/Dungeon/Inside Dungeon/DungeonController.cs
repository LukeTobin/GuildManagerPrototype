using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DungeonController : MonoBehaviour
{
    public enum State{
        Waiting, // no step selected, do nothing until one is found
        Empty, // step has no content
        Enemy, // combat encounter
        Boss, // final dungeon encounter
        End, // step ended
        Cleared // dungeon is cleared
    }

    // Corresponding Data
    public DungeonData data {get; private set;}
    public Decklist decklist {get; private set;}
    
    // Combat AI
    private DungeonCombat combat = null;

    // Visible on screen
    public bool Visible {private get; set;}

    // Character lists
    public List<CharacterCard> team = new List<CharacterCard>();
    public List<CharacterCard> enemies = new List<CharacterCard>();

    // Dungeon State
    [HideInInspector] public State state = State.Waiting;

    // Step Time
    private int minStepTime = 3; // minimum amount of time that must pass before going to the next step
    private float stepTimer = 0; // how much time has currently passed

    // Reward Queue
    private Queue<CardData> rewards = new Queue<CardData>();

    // Limits
    private static int MAX_ENEMY_COUNT = 4;

    void Start(){
        EventManager.Instance.onCharacterDied.AddListener(CharacterDied);
    }

    void Update(){
        // guard clause
        if(decklist == null || state == State.Cleared || combat == null) return;

        // update step state
        if(state == State.Waiting) GetStepState();

        // combat step
        if(stepTimer >= minStepTime) combat.IncrementStep();
        else stepTimer += Time.timeScale * Time.deltaTime;
    }

    public void Setup(DungeonData _data){
        data = _data;

        if(data == null) {
            Debug.LogWarning("Tried setting up without any data", this);
        }
    }

    public void Load(Decklist _decklist){
        // create charactercards
        for(int i = 0;i < _decklist.deck.Count;i++){
            if(_decklist.deck[i] == null) continue;

            CharacterCard adventurerCard = DungeonLiveLoader.Instance.MakeCharacterCard(_decklist.deck[i], this);
            if(adventurerCard) team.Add(adventurerCard);
        }

        // Set & Save Data
        decklist = _decklist;
        data.decklist = decklist;
        SaveManager.Instance.Save(data);
        
        // Event Subscription
        SubscribeCharacters();

        // Create dungeon AI
        combat = new DungeonCombat();
        combat.Dungeon = this;
    }
    
    void GetStepState(){
        // Check if the dungeon has finished
        if(data.progress >= data.layout.Count) {
            state = State.End;
            return;
        }

        switch(data.layout[data.progress]){
            case '_': // empty floor
                state = State.Empty;
                Invoke("DelayStepProgress", 1);
                break;

            case 'e': // enemy floor
                CreateEnemies(data.biome.EnemyDecklists);
                state = State.Enemy;
                break;

            case 'b': // boss floor
                CreateEnemies(data.biome.BossDecklists);
                state = State.Boss;
                break;

            default: // unimplemented state
                Debug.LogWarning($"Tried changing controller to unimplemented state [{data.layout[data.progress].ToString()}]. Defaulting to Empty");
                state = State.Empty;
                Invoke("DelayStepProgress", 1);
                break;
        }
    }

    void DelayStepProgress() => combat.DelayStepProgress();

    public void ProcessCombatIntents(List<CharacterCard> characters){
        StartCoroutine(ProcessIntents(characters));
    }

    IEnumerator ProcessIntents(List<CharacterCard> characters){
        for(int i = 0;i < characters.Count;i++){
            yield return new WaitForSeconds(1f); // delay
            
            // check if enemies are all dead
            if(enemies.Count <= 0) break;

            // set character to next step, to update cooldowns. Done early for better player UX
            characters[i].NextStep();

            // get intent targets
            List<CharacterCard> targets = characters[i].CombatIntent.targets;
            if(targets == null) continue; // guard claus

            // loop through targets and execute skill(s)
            for(int j = 0;j < targets.Count;j++){
                if(characters[i] == null || characters[i].CombatIntent.targets[j] == null) continue;
                characters[i].CombatIntent.skill.Execute(characters[i], characters[i].CombatIntent.targets[j]);
            }

            // mark skill as used and put it on cooldown
            characters[i].SkillUsed(characters[i].CombatIntent.skill);
        }

        // check if enemies are all dead, otherwise get new combat intents
        if(enemies.Count <= 0){
            GetRewards();
            state = State.End;
            Invoke("DelayStepProgress", 1);
        }else{
            combat.GetCombatActions();
        }
    }

    void GetRewards(){
        int weight = Random.Range(1, 100);
        CardData rewardCard = GameManager.Instance.sellableCards[Random.Range(0, GameManager.Instance.sellableCards.Count)].card;
        rewards.Enqueue(rewardCard);
    }

    void CreateEnemies(List<EnemyDecklist> enemyList){
        // clear current enemy list
        enemies.Clear();

        // get a random enemy from biome scriptableobject
        int rndIndex = Random.Range(0, enemyList.Count);
        for(int i = 0;i < enemyList[rndIndex].decklist.Count;i++){
            SpawnEnemy(enemyList[rndIndex].decklist[i]);
        }
        
        // update their intents
        combat.GetCombatActions();
    }

    public void SpawnEnemy(Enemy enemy){
        if(enemies.Count >= MAX_ENEMY_COUNT) return;

        Stats stats = new Stats(enemy.data.stats);
        stats.health = Mathf.Abs(stats.health - (Random.Range(0, 50)));
        AdventurerData newEnemy = new AdventurerData(enemy.data.id, enemy.data.title, enemy.data.sprite, enemy.data.cost, stats, new Stats(stats), enemy.data.skills, enemy.data.learnable, enemy.data._class, null, enemy.data.brain);

        CharacterCard adventurerCard = DungeonLiveLoader.Instance.MakeCharacterCard(newEnemy, this, false);
        if(adventurerCard) {
            adventurerCard.Dungeon = this;
            adventurerCard.IsEnemy = true;
            adventurerCard.OnActionPerformed.AddListener(WriteActionText);
            enemies.Add(adventurerCard);
        }
    }

    void CharacterDied(CharacterCard c){
        for(int i = team.Count-1;i >= 0;i--){
            if(c == team[i]){
                c.Dungeon = null;
                team.RemoveAt(i);
                return;
            }
        }

        for(int i = enemies.Count-1;i >= 0;i--){
            if(c == enemies[i]){
                c.OnActionPerformed.RemoveListener(WriteActionText);
                c.Dungeon = null;
                enemies.RemoveAt(i);
                return;
            }
        }
    }

    void WriteActionText(string txt){
        if(!Visible) return;
        
        DungeonLiveLoader.Instance.WriteToActionText(txt);
    }

    void SubscribeCharacters(){
        if(team.Count <= 0) return;

        foreach(CharacterCard c in team){
            c.OnActionPerformed.AddListener(WriteActionText);
        }
    }

    void UnsubscribeAndClear(){
        foreach(CharacterCard c in team){
            c.OnActionPerformed.RemoveListener(WriteActionText);
            c.Dungeon = null;
            c.gameObject.SetActive(false);
        }

        team.Clear();
    }

    public void LeaveDungeon(){
        UnsubscribeAndClear();
        combat = null;
        DungeonClear.Clear(rewards, this);
    }

    public void ResetStepTimer(){
        stepTimer = 0;
    }
}