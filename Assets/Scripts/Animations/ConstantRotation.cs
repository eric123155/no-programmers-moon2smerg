using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotationValues = new Vector3();

    void Update()
    {
        transform.Rotate(rotationValues * Time.deltaTime);
    }
}
