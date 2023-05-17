using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

// For multiple save states
public class SaveStateManager : MonoBehaviour
{
    string empty;
    public string[] saveFiles;
    
    public void GetLoadFiles(){
        if(!Directory.Exists(Application.persistentDataPath + "/saves/")){
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }

        saveFiles = Directory.GetFiles(Application.persistentDataPath + "/saves/");
    }

    public void OnSave(){
        //SerializationManager.Save(empty, SaveData.Instance);
    }

    public void ShowLoadScreen(){
        GetLoadFiles();

        // foreach(Transform button in loadArea){
        //     Destroy(button.gameObject);
        // }

        // for (int i = 0; i < saveFiles.Length; i++)
        // {
        //     GameObject buttonObject = Instantiate(loadButtonPrefab);
        //     buttonObject.transform.SetParent(loadArea.transform, false);

        //     var index = i;
        //     buttonObject.GetComponent<Button>().onClick.AddListener(() => { however we load the save});
        //     buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = saveFiles[index].Replace(Application.persistentDataPath + "/saves/", "");
        // }
  
    }
}
