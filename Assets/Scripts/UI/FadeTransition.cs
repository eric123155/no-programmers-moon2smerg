using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeTransition : MonoBehaviour
{
    public float targetAlpha = 0;
    public float speed = 1;
    private CanvasGroup _canvasGroup = null;

    private void Awake() 
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Update() 
    {
        _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, targetAlpha, Time.deltaTime * speed);
    }

    public void SetTargetAlpha(float alpha) 
    {
        targetAlpha = alpha;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
