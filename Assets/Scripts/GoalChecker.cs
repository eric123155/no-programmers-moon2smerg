using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalChecker : MonoBehaviour
{
    public List<GoalItem> goalItems = new List<GoalItem>();

    public delegate void ItemPickup();
    public static ItemPickup onItemPickup;
    public UnityEvent onConditionsReached;

    public void OnEnable() 
    {
        onItemPickup += (DoGoalCheck);
    }
    public void OnDisable() 
    {
        onItemPickup -= (DoGoalCheck);
    }

    public void DoGoalCheck() 
    {
        if (HasItems())
            onConditionsReached?.Invoke();
    }

    public bool HasItems() 
    {
        for (int i = 0; i < goalItems.Count; i++) 
            if (goalItems[i].Satified == false)
                return false;
        return true;
    }
}
