using System;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField] private Material _material;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Absorb(transform);
        }
    }

    private void Start()
    {
        
    }

    [ContextMenu("Test")]
    public void Test()
    {
        var val = _material.GetFloat("_Threshold");
        var newVal = 1 - val;
        _material.SetFloat("_Threshold", newVal);
    }
}