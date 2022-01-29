using System.Collections;
using UnityEngine;

public class PlayerGrow : MonoBehaviour
{
    GameObject player;
    Transform playerTransform;
    public Vector3 playerCurrentSize;
    private Vector3 velocity = Vector3.zero;
    public float increaseSizeSpeed = 0.25f;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        playerCurrentSize = player.transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(IncreaseSize(1.3f));
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(DecreaseSize(1.3f));
        }
    }

    public IEnumerator IncreaseSize(float sizeMultiplier)
    {
        playerCurrentSize = player.transform.localScale;
        Vector3 targetSize = playerCurrentSize * sizeMultiplier;

        float t = 0;
        while (t < 1)
        {
            //playerTransform.localScale = Vector3.SmoothDamp(playerCurrentSize, targetSize, ref velocity, increaseSizeSpeed);
            playerTransform.localScale = Vector3.Lerp(playerCurrentSize, targetSize, t);
            t += Time.deltaTime / increaseSizeSpeed;
            yield return null;
        }

        yield return null;
    }

    public IEnumerator DecreaseSize(float sizeMultiplier)
    {
        playerCurrentSize = player.transform.localScale;
        Vector3 targetSize = playerCurrentSize / sizeMultiplier;

        float t = 0;
        while (t < 1)
        {
            //playerTransform.localScale = Vector3.SmoothDamp(playerCurrentSize, targetSize, ref velocity, increaseSizeSpeed);
            playerTransform.localScale = Vector3.Lerp(playerCurrentSize, targetSize, t);
            t += Time.deltaTime / increaseSizeSpeed;
            yield return null;
        }

        yield return null;
    }
}