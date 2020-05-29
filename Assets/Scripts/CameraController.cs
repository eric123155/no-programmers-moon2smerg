using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivityX = 100.0f;
    public float mouseSensitivityY = 100.0f;
    public float clampAngle = 80.0f;
    public GameObject player;
    public GameObject camPivot;
    public GameObject cam;
    public GameObject movementDirectionObject;
    public float leanOffset = 0;

    public GameObject head;

    private float _rotY = 0.0f;
    private float _rotX = 0.0f;
    private Quaternion _targetRotation;

    [SerializeField] private KeyCode sneakLookLeft = KeyCode.Q;
    [SerializeField] private KeyCode sneakLookRight = KeyCode.E;

    void Start()
    {
        player = gameObject;

        Vector3 rot = transform.localRotation.eulerAngles;

        _rotY = rot.y;
        _rotX = rot.x;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Cursor.visible = false;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        SetCameraLean();

        _rotY += mouseX * mouseSensitivityX * Time.deltaTime;
        _rotX += mouseY * mouseSensitivityX * Time.deltaTime;

        _rotX = Mathf.Clamp(_rotX, -clampAngle, clampAngle);

        Quaternion localCamRotation = Quaternion.Euler(_rotX, _rotY, 0.0f);
        camPivot.gameObject.transform.rotation = localCamRotation;

        Quaternion movement = Quaternion.Euler(0.0f, _rotY, 0.0f);
        movementDirectionObject.gameObject.transform.rotation = movement;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        Vector3 cameraDirection = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z);
        Vector3 playerDirection = new Vector3(player.transform.forward.x, 0f, player.transform.forward.z);

        if (Vector3.Angle(cameraDirection, playerDirection) > 45f)
        {
            _targetRotation = Quaternion.LookRotation(cameraDirection, transform.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, mouseSensitivityY * 1 * Time.fixedDeltaTime);
        }
    }

    void SetCameraLean() 
    {
        Vector3 sneakLookPos = new Vector3();
        if (Input.GetKey(sneakLookRight))
            sneakLookPos = Vector3.Lerp(cam.transform.position, camPivot.transform.position + camPivot.transform.right * leanOffset, Time.deltaTime);
        else if (Input.GetKey(sneakLookLeft))
            sneakLookPos = Vector3.Lerp(cam.transform.position, camPivot.transform.position + camPivot.transform.right * -leanOffset, Time.deltaTime);
        else sneakLookPos = Vector3.Lerp(cam.transform.position, camPivot.transform.position, Time.deltaTime);
        cam.transform.position = sneakLookPos;
    }
}