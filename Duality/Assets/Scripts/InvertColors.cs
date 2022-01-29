using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertColors : MonoBehaviour
{
    public Material _material;
    public float transitionSpeed = 0.25f;
    

    [ContextMenu("invert")]
    public void InvertColorsBW()
    {
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

        yield return null;
    }
}
