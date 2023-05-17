using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

public class ClassSerializationSurrogate : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context){
        if(obj == null) return;
        
        Class _class = (Class)obj;
        if(_class == null) return;

        info.AddValue("name", _class.name);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector){
        if(obj == null) return obj;

        Class _class = (Class)obj;
        //if(_class == null) return obj;

        string n = (string)info.GetValue("name", typeof(string));
        _class = Resources.Load<Class>("Classes/" + n);

        obj = _class;
        return obj;
    }
}
