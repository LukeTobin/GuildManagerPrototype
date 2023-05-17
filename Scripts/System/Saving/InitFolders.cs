using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InitFolders : MonoBehaviour
{
    [SerializeField] List<string> folderNames = new List<string>();

    void Awake() {
        for (int i = 0; i < folderNames.Count; i++)
        {
            if(!Directory.Exists(SaveManager.Instance.GetDirectory(GameDirectory.CurrentSave) + folderNames[i] + "/")){
                Directory.CreateDirectory(SaveManager.Instance.GetDirectory(GameDirectory.CurrentSave) + folderNames[i] + "/");
            }
        }
        
    }
}
