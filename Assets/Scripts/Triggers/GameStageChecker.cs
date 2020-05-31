using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStageChecker : MonoBehaviour
{
    public List<GameStage> gameStages = new List<GameStage>();
    public UnityEvent onConditionsReached;

    public delegate void State();
    public static State onGameStage;

    public void OnEnable()
    {
        onGameStage += DoStageCheck;
    }
    public void OnDisable()
    {
        onGameStage += DoStageCheck;
    }

    public void DoStageCheck()
    {
        if (HasStages())
            onConditionsReached?.Invoke();
    }

    public bool HasStages()
    {
        if (gameStages.Count == 0)
            return false;

        for (int i = 0; i < gameStages.Count; i++)
            if (gameStages[i].Satified == false)
                return false;
        return true;
    }
}
