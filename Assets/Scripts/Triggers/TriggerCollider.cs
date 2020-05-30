using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCollider : MonoBehaviour
{
    public bool useTag = false;
    public bool useInput = false;
    [Space]
    public new string tag = "";
    public KeyCode input;
    [Space]
    public UnityEvent onTrigger = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (useTag) 
        {
            if (other.tag == tag)
                onTrigger?.Invoke();
        }
        else onTrigger?.Invoke();
    }
}
