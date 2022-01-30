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
    private GameObject player;
    private Transform playerTransform;

    private void Start()
    {
        invertColors = GetComponent<InvertColors>();
        player = GameObject.FindGameObjectWithTag("Player");
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
                StartCoroutine(Die(Vector3.zero));
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
                StartCoroutine(Die(Vector3.one * 25));
            }
        }
    }

    private IEnumerator Die(Vector3 targetSize)
    {
        var startScale = playerTransform.localScale;
        float t = 0;
        while (t < 1)
        {
            //Time.timeScale = Mathf.Lerp(1, 0, t);
            playerTransform.localScale = Vector3.Lerp(startScale, targetSize, t);
            t += Time.unscaledDeltaTime / 0.8f;
            yield return null;
        }

        EnemyManager.Instance.GameOver();
        gameOverCanvas.SetActive(true);
        yield return null;
    }
}