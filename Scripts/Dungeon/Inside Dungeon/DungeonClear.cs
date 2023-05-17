using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class DungeonClear
{
    public static void Clear(Queue<CardData> rewards, DungeonController dungeon){
        // Add Rewards too roster
        foreach(CardData data in rewards){
            RosterManager.Instance.AddCard(data, true);
        }
        SaveManager.Instance.Save(RosterManager.Instance.rosterData);

        // Give Clear XP to units
        // Unique Clear rewards
        // Better notification system
        
        // Create notification
        NotificationController.Instance.CreateNotification(dungeon.data.id + " Cleared", 
                                                            $"Dungeon: {dungeon.data.id} cleared, Rewards added to Roster", 
                                                            GameManager.Instance.TestLog, true);


        // Event Trigger : Unloads decklist, clears dungeon
        EventManager.Instance.onDungeonCleared.Invoke(dungeon);

        // string path = SaveManager.Instance.GetDirectory(GameDirectory.Dungeon) + dungeon.data.id + ".save";
        // if(File.Exists(path)){
        //     Debug.Log($"<color=green>dungeon [<color=white>{dungeon.data.id}</color>] cleared & deleted</color>");
        //     File.Delete(path);
        // }
    }
}