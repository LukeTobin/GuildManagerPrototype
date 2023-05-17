using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
public class GameLoader : MonoBehaviour
{
    public GameState state;

    public void Load(GameState _state){
        GameManager.Instance.game = _state;
    }
}
