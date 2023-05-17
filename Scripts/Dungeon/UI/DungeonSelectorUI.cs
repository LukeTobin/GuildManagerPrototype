using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DungeonSelectorUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject dunGenLayoutGroup;
    [SerializeField] GameObject dungeonTilePrefab;
    [SerializeField] DunGenSettings defaultDungeonSettings;

    public List<DungeonTileUI> loadedDungeons = new List<DungeonTileUI>();

    // Make dungeon visible on screen, and link data
    public void ShowDungeon(DungeonController controller){
        GameObject dungeonTileGO = Instantiate(dungeonTilePrefab);
        dungeonTileGO.transform.SetParent(dunGenLayoutGroup.transform, false);
        DungeonTileUI dungeonTile = dungeonTileGO.GetComponent<DungeonTileUI>();
        dungeonTile.Create(controller);
        loadedDungeons.Add(dungeonTile);
    }
}