using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCollider : MonoBehaviour
{
    public string tag;
    public UnityEvent onTrigger = new UnityEvent();
    public UnityEvent onTagTrigger = new UnityEvent();

    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (tag.Length > 0)
        {
            if (other.tag == "Player")
            {
                onTrigger?.Invoke();
                Debug.Log("Test");
            }
        }
        else Debug.Log("Something Triggered");
    }
}
