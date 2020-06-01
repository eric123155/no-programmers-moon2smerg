using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWorldObject : MonoBehaviour
{
    public void Delete() 
    {
        Destroy(gameObject);
    }
}
