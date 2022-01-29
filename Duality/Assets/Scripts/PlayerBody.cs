using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Absorb(transform);
        }
    }
}