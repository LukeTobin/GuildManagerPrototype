using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

public class SkillSerializationSurrogate : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context){
        if(obj == null) return;
        
        Skill skill = (Skill)obj;
        if(skill == null) return;

        info.AddValue("name", skill.name);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector){
        if(obj == null) return obj;

        Skill skill = (Skill)obj;
        //if(_class == null) return obj;

        string n = (string)info.GetValue("name", typeof(string));
        skill = Resources.Load<Skill>("Skills/" + n);

        obj = skill;
        return obj;
    }
}
