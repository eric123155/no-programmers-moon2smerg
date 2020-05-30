using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public GameObject worldItemObject;

    public abstract void OnPickup();

    public virtual GameObject CreateWorldItem() 
    {
        GameObject item = Instantiate(worldItemObject);
        WorldItem worldItem = item.GetComponent<WorldItem>();
        worldItem.item = this;
        return item;
    }
}
