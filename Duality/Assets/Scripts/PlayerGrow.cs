using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrow : MonoBehaviour
{

    //GameObject player;
    //public Vector3 scaleChange;
    //public float growMultiplier = 0.5f;
    //// Start is called before the first frame update


    //void Start()
    //{
    //    player = GameObject.FindGameObjectWithTag("Player");
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //    player.transform.localScale += new Vector3(growMultiplier * Time.deltaTime, growMultiplier * Time.deltaTime, 0);
    //        Grow();
    //    }
    //}


    //void Grow()
    //{
    //}


    GameObject player;

    float growTimeReset;
    public float scaleFactor = 10f;

    public float growTime = 0.5f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            growTime -= Time.deltaTime;
            StartCoroutine(ScaleOverTime(growTime));
        }
    }

    IEnumerator ScaleOverTime(float time)
    {
        Vector3 currentScale = player.transform.localScale;
        Vector3 destinationScale = currentScale * scaleFactor;
        //Vector3 destinationScale = new Vector3(scaleFactor, scaleFactor, 0);

        do
        {
            player.transform.localScale = Vector3.Lerp(currentScale, destinationScale, time);

            yield return null;
        }
        while (growTime <= time);


        //Vector3 originalScale = player.transform.localScale;
        //Vector3 destinationScale = new Vector3(scaleFactor, scaleFactor, 0);

        //float currentTime = 0.0f;

        //do
        //{
        //    player.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
        //    currentTime += Time.deltaTime;
        //    yield return null;
        //} while (currentTime <= time);

    }
}



