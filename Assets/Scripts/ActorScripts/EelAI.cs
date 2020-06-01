using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EelAI : SimpleAI
{
    [SerializeField] private float angerSpeed = 5;
    [SerializeField] private float lerpMultiplier = 5;

    private BibleLecternController _bibleLectern = null;
    private Transform _targetOverride = null;
    private float _lostPursueLimit = 5;
    private float _targetSpeed = 0;

    protected override void Awake() 
    {
        base.Awake();
        _bibleLectern = FindObjectOfType<BibleLecternController>(); 
    }

    protected override IEnumerator WanderState()
    {
        if (_animator)
            _animator.SetBool("Moving", true);

        _wanderPosition = RandomNavSphere(_target.transform.position, 15, -1);
        _agent.SetDestination(_wanderPosition);

        while (state == State.Wander)
        {
            if (_bibleLectern && _bibleLectern.IsActive)
            {
                _targetOverride = _bibleLectern.transform;
                _targetSpeed = angerSpeed;
            }
            else
            {
                _targetOverride = _target.transform;
                _targetSpeed = _baseSpeed;
            }

            if (CanSeePlayer(45))
                state = State.Follow;
            else 
            {
                if (_bibleLectern && _bibleLectern.IsActive)
                    state = State.Follow;
                else if (Vector3.Distance(_agent.transform.position, _wanderPosition) < 1)
                {
                    _wanderPosition = RandomNavSphere(_target.transform.position, 15, -1);
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
            if (CanSeePlayer(55))
                timeElapsed = 0;
            timeElapsed += Time.deltaTime;
            if (timeElapsed > _lostPursueLimit) 
            {
                state = State.Wander;
                NextState();
                yield break;
            }

            if (_bibleLectern && _bibleLectern.IsActive) 
            {
                if (CanSeePlayer(105)) 
                {
                    _targetOverride = _target.transform;
                    _targetSpeed = angerSpeed;
                }
                if (Vector3.Distance(transform.position, _bibleLectern.transform.position) < 1)
                {
                    _agent.SetDestination(_bibleLectern.transform.position);
                    _animator.SetBool("Moving", false);
                    yield return new WaitForSeconds(10);
                    _animator.SetBool("Moving", true);
                    _bibleLectern.IsActive = false;
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

    private bool CanSeePlayer(float maxAngle) 
    {
        if (maxAngle > 0)
        {
            Vector3 targetDir = _target.position - _agent.transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);

            if (angle > maxAngle)
                return false;
        }
        if (Physics.Linecast(_agent.transform.position + Vector3.up, _target.position))
            return false;
        return true;
    }
}
