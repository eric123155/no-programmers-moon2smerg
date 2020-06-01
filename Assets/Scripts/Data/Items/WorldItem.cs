using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldItem : MonoBehaviour
{
    public Item item;

    public UnityEvent onPickup = new UnityEvent();

    public void OnPickup() 
    {
        if (item)
            item.OnPickup();
        onPickup?.Invoke();
    }
}
