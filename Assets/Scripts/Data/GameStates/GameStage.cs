using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameStage : ScriptableObject
{
    public bool Satified { get; private set; } = false;

    public void Invoke()
    {
        Satified = true;
        GameStageChecker.onGameStage?.Invoke();
    }

    public void OnDisable()
    {
        Satified = false;
    }
}
