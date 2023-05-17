using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance;

    [Header("Scene References")]
    [SerializeField] DungeonSelectorUI dungeonSelectorUI;

    [Header("Stored Data")]
    public List<DungeonController> controllers = new List<DungeonController>();

    void Awake(){
        if(!Instance) Instance = this;
        else Destroy(gameObject);
    }

    void Start(){
        Preload();
    }

    public void CreateNewDungeon(DunGenSettings settings){
        DungeonData data = DungeonGenerator.Instance.Generate(settings);
        DungeonController controller = gameObject.AddComponent<DungeonController>();

        // Ready the controller
        controller.Setup(data);
        controllers.Add(controller);

        // Create UI
        dungeonSelectorUI.ShowDungeon(controller);

        EventManager.Instance.onDungeonCleared.AddListener(RemoveDungeon);

        SaveManager.Instance.Save(data);
    }

    void Preload(){
        // Get path's
        string[] paths = LoadDungeonDirectories();
        if(paths.Length <= 0) return;

        // Create controllers
        for(int i = 0;i < paths.Length;i++){
            // Convert path to name
            string fileName = paths[i].Replace(SaveManager.Instance.GetDirectory(GameDirectory.Dungeon), "").Replace(".save", "");
            DungeonData data = SaveData.current.LoadDungeon(fileName);

            if(data.progress >= data.layout.Count){
                RemoveDungeonFile(data);
                continue;
            }

            // Create Controller
            DungeonController controller = gameObject.AddComponent<DungeonController>();
            controllers.Add(controller);

            // Ready the controller
            controller.Setup(data);
            
            // Create UI
            dungeonSelectorUI.ShowDungeon(controller);

            EventManager.Instance.onDungeonCleared.AddListener(RemoveDungeon);
        }
    }

    string[] LoadDungeonDirectories(){
        string[] saveFiles;

        if(!Directory.Exists(SaveManager.Instance.GetDirectory(GameDirectory.Dungeon))){
            Directory.CreateDirectory(SaveManager.Instance.GetDirectory(GameDirectory.Dungeon));
        }

        return saveFiles = Directory.GetFiles(SaveManager.Instance.GetDirectory(GameDirectory.Dungeon));
    }

    /// <summary>
    /// Remove a dungeon from the manager list, the dungeon will no longer make system requests
    /// </summary>
    /// <param name="_controller">The dungeon to deactivate</param>
    public void RemoveDungeon(DungeonController _controller){
        if(controllers.Contains(_controller)) 
            controllers.Remove(_controller);
        _controller.enabled = false;
    }

    void RemoveDungeonFile(DungeonData data){
        string path = SaveManager.Instance.GetDirectory(GameDirectory.Dungeon) + data.id + ".save";
        if(File.Exists(path)){
            Debug.Log("Deleting dungeon: " + data.id);
            File.Delete(path);
        }else{
            Debug.Log($"Tried deleting dungeon: {data.id}, but could not find file.");
        }
    }
}