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
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
    }

    void LateUpdate()
    {
        if (BookOverlayManager.Instance.IsActive || JumpScareOverlayManager.Instance.IsActive || BookQuestionManager.Instance.IsActive)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            return;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Vector3 moveDirection = (transform.forward * _v) + transform.right *  _h;
        GetComponent<CharacterController>().SimpleMove(moveDirection.normalized * speed);
    }

}
