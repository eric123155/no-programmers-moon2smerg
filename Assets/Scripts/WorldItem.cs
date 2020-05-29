using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            Debug.Log("Test");
        }
    }
}
