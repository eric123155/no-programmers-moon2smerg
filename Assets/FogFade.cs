using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogFade : MonoBehaviour
{
    public float timeToMax = 1;
    public float fogDuration = 10;
    public float maxDensity = 0.2f;
    private Coroutine _fadeCoroutine = null;
    private Coroutine _durationCoroutine = null;
    private float _targetDensity = 0;


    public void FadeIn()
    {
        if (_fadeCoroutine != null) 
            StopCoroutine(_fadeCoroutine);
        if (_durationCoroutine != null)
            StopCoroutine(_durationCoroutine);

        _targetDensity = maxDensity;
        _fadeCoroutine = StartCoroutine(Fade());
        _durationCoroutine = StartCoroutine(FogDurationYield());
    }
    public void FadeOut()
    {
        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);
        if (_durationCoroutine != null)
            StopCoroutine(_durationCoroutine);

        _targetDensity = 0;
        _fadeCoroutine = StartCoroutine(Fade());
    }


    private IEnumerator Fade()
    {
        float timeElapsed = 0;
        while (timeElapsed != timeToMax)
        {
            timeElapsed += Time.deltaTime;
            RenderSettings.fogDensity = Mathf.Clamp(Mathf.Lerp(RenderSettings.fogDensity, _targetDensity, Time.fixedDeltaTime / timeToMax), 0, maxDensity);
            yield return null;
        }
        Debug.Log(Time.frameCount + " || " + timeElapsed + " || " + RenderSettings.fogDensity);
    }

    private IEnumerator FogDurationYield() 
    {
        yield return new WaitForSeconds(timeToMax + fogDuration);
        FadeOut();
    }
}
