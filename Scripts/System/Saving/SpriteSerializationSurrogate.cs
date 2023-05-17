using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

//https://www.youtube.com/watch?v=5roZtuqZyuw?t=385 : 6:25
public class SpriteSerializationSurrogate : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context){
        if(obj == null) return;
        
        Sprite sprite = (Sprite)obj;
        if(sprite == null) return;

        info.AddValue("name", sprite.name);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector){
        if(obj == null) {return null;}
        
        Sprite spr = (Sprite)obj;

        string n = (string)info.GetValue("name", typeof(string));
        spr = Resources.Load<Sprite>("Images/" + n);

        return spr;
    }
}
