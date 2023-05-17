using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLookup : MonoBehaviour
{
    public static DataLookup Instance;

    [Header("Classes")]
    public List<Class> vitalityClasses = new List<Class>();
    public List<Class> agilityClasses = new List<Class>();
    public List<Class> magicClasses = new List<Class>();
    public List<Class> physicalClasses = new List<Class>();
    public List<Class> capabilityClasses = new List<Class>();

    [Header("General Skills")]
    public List<Skill> generalSkills = new List<Skill>();

    void Awake(){
        Instance = this;
    }
}