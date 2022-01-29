using System;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    [SerializeField] private PlayerGrow playerGrow;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(playerGrow.IncreaseSize(1.05f));
            other.GetComponent<Enemy>().Absorb(transform);
        }
    }
}