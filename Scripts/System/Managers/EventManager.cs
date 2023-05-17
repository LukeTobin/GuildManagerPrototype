using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake() {
        Instance = this;
    }

    public UnityEvent onDayChanged = new UnityEvent();
    public UnityEvent onGoldChanged = new UnityEvent();

    public UnityEvent<DungeonController> onDungeonCleared = new UnityEvent<DungeonController>();
    public UnityEvent onDungeonProgressed = new UnityEvent();
    public UnityEvent<CharacterCard> onCharacterDied = new UnityEvent<CharacterCard>();

    public UnityEvent onItemPurchased = new UnityEvent();
    public UnityEvent onNewRosterMember = new UnityEvent();
}