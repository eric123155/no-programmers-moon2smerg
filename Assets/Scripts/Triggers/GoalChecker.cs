using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalChecker : MonoBehaviour
{
    public List<GoalItem> goalItems = new List<GoalItem>();
    public UnityEvent onConditionsReached;
    
    public delegate void Goal();
    public static Goal onItemPickup;

    public void OnEnable() 
    {
        onItemPickup += DoGoalCheck;
    }
    public void OnDisable() 
    {
        onItemPickup -= (DoGoalCheck);
    }

    public void DoGoalCheck() 
    {
        if (HasGoalItems())
            onConditionsReached?.Invoke();
    }

    public bool HasGoalItems() 
    {
        if (goalItems.Count == 0)
            return false;

        for (int i = 0; i < goalItems.Count; i++) 
            if (goalItems[i].Satified == false)
                return false;
        return true;
    }
}
