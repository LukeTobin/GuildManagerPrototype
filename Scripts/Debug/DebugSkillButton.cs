using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Convert to using actual skills later
public class DebugSkillButton : MonoBehaviour
{
    Button m_Button;

    [Header("Skill")]
    [SerializeReference] public Skill skill;

    [Header("Debug")]
    public CharacterCard target;
    public CharacterCard source;

    private void Awake() {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(Perform);
    }

    public void Perform(){
        //skill.Perform(source, target);
    }
}
