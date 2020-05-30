using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SimpleAI : AI
{
    public enum State { Idle = default, Follow }
    [SerializeField] public State state = default;

    private Transform _target = null;

    private void OnEnable()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        NextState();
    }

    private void NextState()
    {
        string methodName = state.ToString() + "State";
        MethodInfo info = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

    public void SetToDefaultState()
    {
        state = default;
    }

    private IEnumerator IdleState()
    {
        while (state == State.Idle)
        {
            yield return null;
        }
    }
    private IEnumerator FollowState()
    {
        _agent.SetDestination(_target.position);

        while (state == State.Follow) 
        {
            _agent.SetDestination(_target.position);
            yield return null;
        }
    }
}
