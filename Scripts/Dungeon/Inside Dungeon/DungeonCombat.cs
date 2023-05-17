using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCombat
{
    public DungeonController Dungeon {private get;set;}

    // Combat characters
    List<CharacterCard> characters = new List<CharacterCard>();
    bool nextStepReady = false; // can go to the next step

    /// <summary>
    /// Get the next step and state of the dungeon
    /// </summary>
    public void IncrementStep(){
        if(nextStepReady){
            // If State is empty or ending, check if the dungeon is cleared
            if(Dungeon.state == DungeonController.State.Empty || Dungeon.state == DungeonController.State.End){
                if(IsStageClear(Dungeon.data)){
                    Dungeon.state = DungeonController.State.Cleared;
                    Dungeon.LeaveDungeon();
                    return;
                }
            }
            // If controller state is combat based, complete combat
            else if(Dungeon.state == DungeonController.State.Enemy || Dungeon.state == DungeonController.State.Boss){
                CombatStep();
            }

            // Reset values
            Dungeon.ResetStepTimer();
            nextStepReady = false;

            // If controller state should end, go to wait state
            if(Dungeon.state == DungeonController.State.Empty || Dungeon.state == DungeonController.State.End){
                Debug.Log($"State ended... Waiting for next state... Depth [{Dungeon.data.progress}/{Dungeon.data.layout.Count}]");
                Dungeon.state = DungeonController.State.Waiting;
            }
        }
    }

    void CombatStep(){
        characters.Clear();
        characters.AddRange(Dungeon.team);
        characters.AddRange(Dungeon.enemies);

        characters.Sort(CompareBySpeed);

        Dungeon.ProcessCombatIntents(characters);
    }

    /// <summary>
    /// Populate the intent field of all CharacterCards
    /// </summary>
    public void GetCombatActions(){
        bool allReady = true;

        // Get enemy actions
        foreach(CharacterCard e in Dungeon.enemies){
            e.NewIntent(Dungeon.enemies, Dungeon.team);
            if(e.CombatIntent == null){
                allReady = false;
            }
        }

        // Get team actions
        foreach(CharacterCard c in Dungeon.team){
            c.NewIntent(Dungeon.team, Dungeon.enemies);
            if(c.CombatIntent == null){
                allReady = false;
            }
        }

        nextStepReady = allReady;
    }

    bool IsStageClear(DungeonData data){
        bool clear = false;

        data.progress++;
        
        if(data.progress >= data.layout.Count)
            clear = true;
        else
            EventManager.Instance.onDungeonProgressed.Invoke();

        // Save Dungeon progress
        SaveManager.Instance.Save(data);
        SaveManager.Instance.Save(RosterManager.Instance.rosterData);

        return clear;
    }

    public void DelayStepProgress() => nextStepReady = true;

    static int CompareBySpeed(CharacterCard c1, CharacterCard c2){
        return c2.Data.currentStats.swiftness.CompareTo(c1.Data.currentStats.swiftness);
    }
}