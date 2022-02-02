using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertColors : MonoBehaviour
{
    [Header("Sound")]
    public AudioClip newCycleSound;
    public float newCycleSoundVolume = 1f;

    public Material _material;
    public float transitionSpeed = 0.25f;

    private void Awake()
    {
        SetThreshold(0);
    }

    private void SetThreshold(float value)
    {
        _material.SetFloat("_Threshold", value);
    }

    [ContextMenu("invert")]
    public void InvertColorsBW()
    {
        NewCycleSound();

        var val = _material.GetFloat("_Threshold");
        var newVal = 1 - val;

        StartCoroutine(TweenInvert(newVal));
    }

    public IEnumerator TweenInvert(float target)
    {
        var currentValue = _material.GetFloat("_Threshold");

        float t = 0;
        while (t < 1)
        {
            var value = Mathf.Lerp(currentValue, target, t);
            _material.SetFloat("_Threshold", value);
            t += Time.deltaTime / transitionSpeed;
            yield return null;
        }

        _material.SetFloat("_Threshold", target); // :) 

        yield return null;
    }

    void NewCycleSound()
    {
        AudioManager.Instance.sfxAudioSource.PlayOneShot(newCycleSound, newCycleSoundVolume);
    }

}
