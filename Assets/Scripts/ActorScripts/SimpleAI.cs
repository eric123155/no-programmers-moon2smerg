using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class SimpleAI : AI
{
    public enum State { Idle = default, Wander, Follow }
    [SerializeField] protected State state = default;

    protected Transform _target = null;
    protected Vector3 _wanderPosition = new Vector3();

    private void OnEnable()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        NextState();
    }

    protected void NextState()
    {
        string methodName = state.ToString() + "State";
        MethodInfo info = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

    protected void SetToDefaultState()
    {
        state = default;
    }

    protected virtual IEnumerator IdleState()
    {
        if (_animator)
            _animator.SetBool("Moving", false);

        while (state == State.Idle)
        {
            yield return null;
        }
        NextState();
    }
    protected virtual IEnumerator WanderState()
    {
        if (_animator)
            _animator.SetBool("Moving", true);

        _wanderPosition = RandomNavSphere(_agent.transform.position, 5, -1);
        _agent.SetDestination(_wanderPosition);

        while (state == State.Wander)
        {
            if (Vector3.Distance(_agent.transform.position, _wanderPosition) < 1) 
            {
                _wanderPosition = RandomNavSphere(_agent.transform.position, 50, -1);
                _agent.SetDestination(_wanderPosition);
            }
            yield return null;
        }
        NextState();
    }
    protected virtual IEnumerator FollowState()
    {
        if (_animator)
            _animator.SetBool("Moving", true);

        while (state == State.Follow) 
        {
            _agent.SetDestination(_target.position);
            yield return null;
        }
        NextState();
    }
    protected Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
        return navHit.position;
    }
}
