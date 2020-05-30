using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float speed;
    private Rigidbody _rigidbody = null;
    private float _h = 0;
    private float _v = 0;

    void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");
        
    }

    void LateUpdate()
    {
        Vector3 moveDirection = (transform.forward * _v) + transform.right *  _h;
        GetComponent<CharacterController>().SimpleMove(moveDirection.normalized * speed);
    }

}
