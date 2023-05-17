using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine;
using System;

public class SerializationManager
{
    public static bool Save(string saveName, object saveData){
        // Built in formatter
        BinaryFormatter formatter = GetBinaryFormatter();


        // Validate directory
        if(!Directory.Exists(Application.persistentDataPath + "/saves")){
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        // Create & serialize file
        string path = SaveManager.Instance.GetDirectory(GameDirectory.CurrentSave) + saveName + ".save";
        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);
        file.Close();

        return true;
    }

    public static object Load(string path){
        // guard claus
        if(!File.Exists(path)) return null;

        // Access FileStream to find out a file based on the given path
        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        try{
            // Try Deserialize the file
            object save = formatter.Deserialize(file);
            // If we could, close the file
            file.Close();
            return save;
        }
        catch(Exception e){
            Debug.LogErrorFormat("Failed to load file at {0}: {1}", path, e.ToString());
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter(){
        BinaryFormatter formatter = new BinaryFormatter();
        SurrogateSelector selector = new SurrogateSelector();

        SpriteSerializationSurrogate spriteSerialization = new SpriteSerializationSurrogate();
        ClassSerializationSurrogate classSerialization = new ClassSerializationSurrogate();
        SkillSerializationSurrogate skillSerialization = new SkillSerializationSurrogate();
        BiomeSerializationSurrogate biomeSerialization = new BiomeSerializationSurrogate();

        selector.AddSurrogate(typeof(Sprite), new StreamingContext(StreamingContextStates.All), spriteSerialization);
        selector.AddSurrogate(typeof(Class), new StreamingContext(StreamingContextStates.All), classSerialization);
        selector.AddSurrogate(typeof(Skill), new StreamingContext(StreamingContextStates.All), skillSerialization);
        selector.AddSurrogate(typeof(DungeonBiome), new StreamingContext(StreamingContextStates.All), biomeSerialization);

        formatter.SurrogateSelector = selector;

        return formatter;
    }
}
