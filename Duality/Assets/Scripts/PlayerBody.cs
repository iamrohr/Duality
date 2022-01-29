using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    [SerializeField] private PlayerGrow playerGrow;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        
        StartCoroutine(playerGrow.ChangeSize(1.05f));
        other.GetComponent<Enemy>().Absorb(transform);
    }
}