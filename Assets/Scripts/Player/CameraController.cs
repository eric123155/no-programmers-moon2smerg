using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private float smoothing = 5f;
    private GameObject _character;
    private Vector2 _mouseLook;
    private Vector2 _smoothV;

    void Start()
    {
        _character = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (BookOverlayManager.Instance.IsActive || JumpScareOverlayManager.Instance.IsActive)
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

        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        _smoothV.x = Mathf.Lerp(_smoothV.x, md.x, 1f / smoothing);
        _smoothV.y = Mathf.Lerp(_smoothV.y, md.y, 1f / smoothing);
        _mouseLook += _smoothV;

        Vector2 clampedMouseLook = new Vector2(_mouseLook.x, Mathf.Clamp(_mouseLook.y, -90, 90));
        _mouseLook = clampedMouseLook;

        transform.localRotation = Quaternion.AngleAxis(-_mouseLook.y, Vector3.right);
        _character.transform.localRotation = Quaternion.AngleAxis(_mouseLook.x, _character.transform.up);
    }
}