using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class DebugTests : MonoBehaviour
{
    [Header("Final Values")]
    public int DamageA;
    public int ExpDamage;
    public int ReverseExpoDamage;
    public int Pokemon;
    public int LinearDamage;

    [Header("Input")]
    public int dmgOvr;
    public int characterDamage;
    public int enemyResistance;
    public int skillDamage;
    public float scale;

    [Button]
    public void GetDamage(){
        Clear();
       // CustomA();
        ExpoDamage();
       // ReverseExpo();
       PokemonCalc();
       Linear();
    }

    void CustomA(){
        float _dmgOvr = dmgOvr;
        float chDmg = characterDamage;
        float enRes = enemyResistance;
        float skillDmg = skillDamage;

        Debug.Log("-----------");
        Debug.Log("Damage A");

        // atk / (def + 100 / 100)
        float maybe = chDmg * chDmg / enRes;
        Debug.Log($"maybe: {maybe}");

        float _damage = chDmg / (((enRes + 100) / 100));
        Debug.Log($"base: {_damage}");

        float _skill = skillDmg + dmgOvr + (_damage / skillDmg);
        Debug.Log($"skill: {_skill}");

        float finalDamage = _damage * _skill;
        Debug.Log($"final: {finalDamage}");
        DamageA = (int)finalDamage;

        Debug.Log("-----------");
    }

    void ExpoDamage(){
        float _dmgOvr = dmgOvr;
        float chDmg = characterDamage;
        float enRes = enemyResistance;
        float skillDmg = skillDamage;

        //float _scale = scale + (_dmgOvr / 100);
        float _scale = scale;

        float resistance = (float)characterDamage / (float)enemyResistance;
        Debug.Log($"damage / resistance: {resistance}");

        float mult = resistance * (float)dmgOvr;
        Debug.Log($"* overall: {mult}");
        
        float _damageScaled = Mathf.Pow(mult, _scale);
        Debug.Log($"base: {_damageScaled}");
        
        float _damage = _damageScaled * skillDmg / 100;
        Debug.Log($"skill included: {_damage}");

        float scaler = (_damage * (_dmgOvr / 100));
        Debug.Log($"scaler: {scaler}");

        float scaleB = Mathf.Pow(_damage, 1f + (_dmgOvr / 100));
        Debug.Log($"scale metho B: {scaleB}");

        float final = _damage + scaler;
        Debug.Log($"final: {final}");

        Debug.Log("----------");

        ExpDamage = (int)final;
    }

    void ReverseExpo(){
        float _dmgOvr = dmgOvr;
        float chDmg = characterDamage;
        float enRes = enemyResistance;
        float skillDmg = skillDamage;

        // atk / (2^(atk/def))
        float _damage = chDmg / Mathf.Pow(chDmg / enRes, 2) * skillDmg;
        ReverseExpoDamage = (int)_damage;
    }

    void PokemonCalc(){
        // damage = ((2 * level / 5 + 2) * power * (atk/def) / 50) + 2

        float _dmgOvr = dmgOvr;
        float chDmg = characterDamage;
        float enRes = enemyResistance;
        float skillDmg = skillDamage;

        Debug.Log("Pokemon Formula");
        
        float _damage = (((((2 * _dmgOvr) / 5) + 2) * skillDmg * chDmg / enRes) / 50) + 2;
        Debug.Log($"base: {_damage}");

        float scaleBy = 1f + (_dmgOvr / 100);
        Debug.Log($"scaling by: {scaleBy}");

        float scaled = Mathf.Pow(_damage, scaleBy);

        Pokemon = (int)scaled;

        Debug.Log("-----------");
    }

    void Linear(){
        //float damage = ((float)unitDamage * (float)skillBaseDamage) / (float)enemyDefense * ((float)unitLevel / 10f);
        float _dmgOvr = dmgOvr;
        float chDmg = characterDamage;
        float enRes = enemyResistance;
        float skillDmg = skillDamage;

        float damage = (chDmg * skillDmg) / enRes * (_dmgOvr / 10f);
        LinearDamage = (int)damage;
    }

    void Clear(){
        #if UNITY_EDITOR
        // Clear the console in the Unity Editor
        System.Type logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        MethodInfo clearMethod = logEntries.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
        clearMethod.Invoke(null, null);
        #else
        // Clear the console in builds
        Debug.ClearDeveloperConsole();
        #endif
    }
}
