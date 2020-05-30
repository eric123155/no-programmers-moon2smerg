using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AI : MonoBehaviour
{
    protected Actor _actor = null;
    protected NavMeshAgent _agent = null;

    private void Awake() 
    {
        _actor = GetComponent<Actor>();
        _agent = GetComponent<NavMeshAgent>();
    }
}
