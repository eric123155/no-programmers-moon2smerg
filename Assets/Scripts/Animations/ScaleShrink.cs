﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScaleShrink : MonoBehaviour
{
    public float timeToShrink = 1;
    private Coroutine _coroutine = null;

    public UnityEvent onFinished = new UnityEvent();

    public void StartShrink() 
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Shrink());
    }

    private IEnumerator Shrink() 
    {
        float timeElapsed = 0;
        while (timeElapsed < timeToShrink) 
        {
            timeElapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, timeElapsed / timeToShrink);
            yield return null;
        }

        onFinished?.Invoke();
    }
}
