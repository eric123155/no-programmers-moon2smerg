using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareOverlayManager : MonoBehaviour
{
    public bool IsActive { get; private set; } = false;

    private static JumpScareOverlayManager _instance = null;
    public static JumpScareOverlayManager Instance { get { if (!_instance) _instance = FindObjectOfType<JumpScareOverlayManager>(); return _instance; } }

    public void Activate() 
    {
        IsActive = true;
    }
}
