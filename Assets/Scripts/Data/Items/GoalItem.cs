﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GoalItem : Item
{
    public bool Satified { get; private set; } = false;

    public override void OnPickup() 
    {
        Satified = true;
        GoalChecker.onItemPickup?.Invoke();
    }

    public void OnDisable() 
    {
        Satified = false;
    }
}
