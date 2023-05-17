using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Range(1,100)] public int min = 30;
    [Range(2,100)] public int max = 50;

    [Header("Game Save")]
    public string GameSave = "";

    [Header("GameState loaded")]
    public GameState game = null;

    [Header("Game Settings")]
    [SerializeField] int secondsInDay = 180;
    [SerializeField] float gameSpeed = 1.0f;

    float secondsTimer = 0;

    [Header("Default Cards")]
    public List<PremadeCard> sellableCards = new List<PremadeCard>();
    

    void Awake(){
        if(!Instance) Instance = this;
        else Destroy(gameObject);

        CheckSave();
    }

    void Start(){
        EventManager.Instance.onDayChanged.AddListener(SaveState);
        EventManager.Instance.onGoldChanged.AddListener(SaveState);
    }

    void Update(){
        IncrementTime();

        if(Input.GetKeyDown(KeyCode.Tab)){
            DevMenu.Instance.Open();
        }
    }

    void IncrementTime(){
        secondsTimer += gameSpeed * Time.deltaTime;

        if(secondsTimer >= secondsInDay){
            NextDay();
        }
    }

    public void NextDay(){
        Debug.Log("New Day");
        secondsTimer = 0;
        game.day++;
        EventManager.Instance.onDayChanged.Invoke();
    }

    void CheckSave(){
        if(!Directory.Exists(SaveManager.Instance.GetDirectory(GameDirectory.Saves) + GameSave)){
            Directory.CreateDirectory(SaveManager.Instance.GetDirectory(GameDirectory.Saves) + GameSave);
        }

        if(!File.Exists(SaveManager.Instance.GetDirectory(GameDirectory.CurrentSave) + GameSave + "_state.save")){
            game = new GameState(GameSave);
            SaveManager.Instance.Save(game);
        }
        else{
            game = SaveData.current.LoadState(GameSave);
        }
    }

    public void GameSpeed(float speed){
        Time.timeScale = speed;
    }

    public void AddGold(int goldAmount){
        if(!Debug.isDebugBuild) return;

        game.gold += goldAmount;
        EventManager.Instance.onGoldChanged.Invoke();
    }

    void SaveState(){
        SaveManager.Instance.Save(game);
    }

    public void TestLog(){
        Debug.Log("Hello World");
    }
}