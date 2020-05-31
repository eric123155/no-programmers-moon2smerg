using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EelAI : SimpleAI
{
    [SerializeField] private float angerSpeed = 5;
    [SerializeField] private float lerpMultiplier = 5;
    [SerializeField] private LecternController bibleLectern = null;

    private Transform _targetOverride = null;
    private float _lostPursueLimit = 5;
    private float _targetSpeed = 0;

    protected override IEnumerator WanderState()
    {
        if (_animator)
            _animator.SetBool("Moving", true);

        _wanderPosition = RandomNavSphere(_target.transform.position, 15, -1);
        _agent.SetDestination(_wanderPosition);

        while (state == State.Wander)
        {
            if (bibleLectern.IsActive)
            {
                _targetOverride = bibleLectern.transform;
                _targetSpeed = angerSpeed;
            }
            else
            {
                _targetOverride = _target.transform;
                _targetSpeed = _baseSpeed;
            }

            if (CanSeePlayer())
                state = State.Follow;
            else 
            {
                if (bibleLectern.IsActive)
                    state = State.Follow;
                else if (Vector3.Distance(_agent.transform.position, _wanderPosition) < 1)
                {
                    _wanderPosition = RandomNavSphere(_agent.transform.position, 50, -1);
                    _agent.SetDestination(_wanderPosition);
                }
            }

            _agent.speed = Mathf.Lerp(_agent.speed, _targetSpeed, Time.deltaTime * lerpMultiplier);
            yield return null;
        }
        NextState();
    }
    protected override IEnumerator FollowState()
    {
        if (_animator)
            _animator.SetBool("Moving", true);

        float timeElapsed = 0;
        while (state == State.Follow)
        {
            if (CanSeePlayer())
                timeElapsed = 0;
            timeElapsed += Time.deltaTime;
            if (timeElapsed > _lostPursueLimit) 
            {
                state = State.Wander;
                NextState();
                yield break;
            }

            if (bibleLectern.IsActive) 
            {
                if (CanSeePlayer())
                    _targetOverride = _target.transform;
                if (Vector3.Distance(transform.position, bibleLectern.transform.position) < 1)
                {
                    _agent.SetDestination(bibleLectern.transform.position);
                    _animator.SetBool("Moving", false);
                    yield return new WaitForSeconds(10);
                    _animator.SetBool("Moving", true);
                    bibleLectern.IsActive = false;
                    state = State.Wander;
                    NextState();
                    yield break;
                }
            }

            _agent.SetDestination(_targetOverride.position);
            _agent.speed = Mathf.Lerp(_agent.speed, _targetSpeed, Time.deltaTime * lerpMultiplier);
            yield return null;
        }
        NextState();
    }

    private bool CanSeePlayer() 
    {
        if (Physics.Linecast(transform.position + Vector3.up, _target.position))
            return false;
        return true;
    }
}
