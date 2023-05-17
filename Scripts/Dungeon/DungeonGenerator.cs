using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    DunGen Key
    ----------
    _ : empty
    e : enemy
    b : boss
    l : loot
    p : preset

*/

public class DungeonGenerator : MonoBehaviour
{
    public static DungeonGenerator Instance;

    DunGenSettings currentSettings = null;

    [Header("Biomes")]
    [SerializeField] DungeonBiome[] biomes;

    void Awake(){
        if(!Instance) Instance = this;
    }

    public DungeonData Generate(DunGenSettings settings){
        currentSettings = settings;

        DungeonData data = new DungeonData();

        data.id = GenString();
        data.biome = biomes[Random.Range(0, biomes.Length)];
        data.layout = GenLayout();

        PrintLayout(data.layout);
        
        // will do at a later stage
        data.rating = 'N'; 
        

        return data;
    }

    string GenString(){
        string rndName = "dungen_";
        int n = Random.Range(6, 10);
        for(int i = 0;i < n;i++){
            rndName += (char)('A' + Random.Range(0, 26));
        }
        return rndName;
    }

    List<char> GenLayout(){
        int size = Random.Range(currentSettings.minSize, currentSettings.maxSize + 1);
        List<char> layout = new List<char>();

        // generate content
        for(int i = 0;i < size;i++){
            if(i == 0)
                layout.Add('_'); // always empty first step
            else if(i == size-1)
                layout.Add('b'); // always add boss to final step
            else{
                int n = Random.Range(1, 101);
                if(currentSettings.enemyChance <= n)
                    layout.Add('_');
                else
                    layout.Add('e');
            }
        }

        return layout;
    }

    void PrintLayout(List<char> c){
        string str = "[";
        for(int i = 0; i < c.Count; i++)
        {
            str += c[i];
        }
        str += "]";
        Debug.Log("size: " + c.Count + " DunGen: " + str);
    }
}
