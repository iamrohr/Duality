using System.Collections;
using UnityEngine;

public class PlayerGrow : MonoBehaviour
{
    public Vector3 playerCurrentSize;
    public float increaseSizeSpeed = 0.25f;
    [SerializeField] private Transform targetBig;
    [SerializeField] private Transform targetSmall;
    [SerializeField] private Shield shield;
    [SerializeField] private GameObject gameOverCanvas;
    
    private InvertColors invertColors;
    private bool goalIsGrowth = true;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private GameObject player;
    private Transform playerTransform;

    private void Start()
    {
        invertColors = GetComponent<InvertColors>();
        playerTransform = player.transform;
        playerCurrentSize = player.transform.localScale;
    }

    public IEnumerator ChangeSize(float sizeMultiplier)
    {
        playerCurrentSize = player.transform.localScale;
        var targetSize = playerCurrentSize * sizeMultiplier;

        float t = 0;
        while (t < 1)
        {
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
                goalIsGrowth = false;
                targetBig.gameObject.SetActive(false);
                targetSmall.gameObject.SetActive(true);
                shield.SetSize(0.2f);
                invertColors.InvertColorsBW();
                UICycles.Instance.SpawnNextCycle();
            }
            else if (sizeMagnitude < (Vector3.one * 0.5f).sqrMagnitude)
            {
                Debug.Log("too small");
                StartCoroutine(Die(Vector3.zero, false));
            }
        }
        else
        {
            if (sizeMagnitude <= targetSmall.localScale.sqrMagnitude)
            {
                goalIsGrowth = true;
                targetBig.gameObject.SetActive(true);
                targetSmall.gameObject.SetActive(false);
                shield.SetSize(0.8f);
                invertColors.InvertColorsBW();
                UICycles.Instance.SpawnNextCycle();
            }
            else if (sizeMagnitude > (Vector3.one * 6).sqrMagnitude)
            {
                Debug.Log("too big");
                StartCoroutine(Die(Vector3.one * 25, true));
            }
        }
    }

    private IEnumerator Die(Vector3 targetSize, bool thenShrink)
    {
        var startScale = playerTransform.localScale;
        float t = 0;
        while (t < 1)
        {
            playerTransform.localScale = Vector3.Lerp(startScale, targetSize, t);
            t += Time.unscaledDeltaTime / 0.8f;
            yield return null;
        }

        if (thenShrink)
        {
            startScale = playerTransform.localScale;
            t = 0;
            while (t < 1)
            {
                playerTransform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
                t += Time.unscaledDeltaTime / 0.8f;
                yield return null;
            }
        }
        
        player.gameObject.SetActive(false);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerRotation>().enabled = false;
        EnemyManager.Instance.GameOver();
        gameOverCanvas.SetActive(true);
        yield return null;
    }
}