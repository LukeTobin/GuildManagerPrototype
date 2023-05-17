using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

public class BiomeSerializationSurrogate : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context){
        if(obj == null) return;
        
        DungeonBiome biome = (DungeonBiome)obj;
        if(biome == null) return;

        info.AddValue("name", biome.name);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector){
        if(obj == null) return obj;

        DungeonBiome biome = (DungeonBiome)obj;

        string n = (string)info.GetValue("name", typeof(string));
        biome = Resources.Load<DungeonBiome>("DunGen/Biomes/" + n);

        obj = biome;
        return obj;
    }
}
