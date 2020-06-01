using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class FadeTransition : MonoBehaviour
{
    public bool onUpdate = true;
    public float targetAlpha = 0;
    public float speed = 1;
    [Space]
    public UnityEvent onFadeDone = new UnityEvent();

    private CanvasGroup _canvasGroup = null;
    private Coroutine _coroutine = null;

    private void Awake() 
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Update() 
    {
        if (onUpdate)
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

    public void Fade(float finishTime) 
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Transition(finishTime));
    }

    public IEnumerator Transition(float finishTime) 
    {
        float timeElapsed = 0f;
        float previous = _canvasGroup.alpha; 

        while (timeElapsed < finishTime)
        {
            Debug.Log(_canvasGroup.alpha);
            timeElapsed += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(previous, targetAlpha, timeElapsed / finishTime);
            yield return null;
        }

        onFadeDone?.Invoke();
    }
}
