using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public Item item;

    public void OnPickup() 
    {
        item.OnPickup();
    }
}
