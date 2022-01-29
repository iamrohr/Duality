using System.Collections;
using UnityEngine;

public class PlayerGrow : MonoBehaviour
{
    public Vector3 playerCurrentSize;
    public float increaseSizeSpeed = 0.25f;
    [SerializeField] private Transform targetBig;
    [SerializeField] private Transform targetSmall;
    
    private bool goalIsGrowth = true;
    private Vector3 velocity = Vector3.zero;
    private GameObject player;
    private Transform playerTransform;

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
            StartCoroutine(ChangeSize(1.25f));
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(ChangeSize(0.8f));
        }
    }

    public IEnumerator ChangeSize(float sizeMultiplier)
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
        
        CheckSize();

        yield return null;
    }

    private void CheckSize()
    {
        var sizeMagnitude = player.transform.localScale.sqrMagnitude;
        if (goalIsGrowth)
        {
            if (sizeMagnitude >= targetBig.localScale.sqrMagnitude)
            {
                Debug.Log("big target met");
                goalIsGrowth = false;
                targetBig.gameObject.SetActive(false);
                targetSmall.gameObject.SetActive(true);
                //TODO: Add point, invert colors, change shield size
            }
            else if (sizeMagnitude < (Vector3.one * 0.1f).sqrMagnitude)
            {
                //TODO: Die, game over
            }
        }
        else
        {
            if (sizeMagnitude <= targetSmall.localScale.sqrMagnitude)
            {
                goalIsGrowth = true;
                targetBig.gameObject.SetActive(true);
                targetSmall.gameObject.SetActive(false);
                //TODO: Add point, invert colors, change shield size
            }
            else if (sizeMagnitude > (Vector3.one * 10).sqrMagnitude)
            {
                //TODO: Die, game over
            }
        }
    }
}