using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LecternController : MonoBehaviour
{
    public Light spotLight;
    public Color defaultColor = Color.white;
    public Color activeColor = Color.red;

    private Color _targetColor = new Color();
    private float _targetIntensity = 0;
    private float _baseIntensity = 0;

    public bool IsActive { get; set; } = false;
    private void Awake() 
    {
        _baseIntensity = spotLight.intensity;
    }
    public void ShowBookOverlay() 
    {
        IsActive = true;
        BookOverlayManager.Instance.ShowUI();

        _targetColor = activeColor;
        _targetIntensity = _baseIntensity * 2;
    }

    private void Update() 
    {
        if (!IsActive) 
        {
            _targetColor = defaultColor;
            _targetIntensity = _baseIntensity;
        }

        if (spotLight)
        {
            spotLight.intensity = Mathf.Lerp(spotLight.intensity, _targetIntensity, Time.deltaTime);
            spotLight.color = Color.Lerp(spotLight.color, _targetColor, Time.deltaTime);
        }
    }
}
