using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Dash mot musen och rotera skölden med A och D

    Rigidbody2D rb;

    public float movementSpeed = 10f;
    public float rotateAngle = 100f;
    public float forceSlowDown = 0.2f;

    public float moveDelay = 0.2f;
    float moveDelayReset;
    bool canMove = true;

    Vector2 mousePosition;
    Vector2 mouseDirection;

    Coroutine SlowDownAbort;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDelayReset = moveDelay;

    }

    // Update is called once per frame
    void Update()
    {
        mouseDirection = (Vector2)Input.mousePosition;


        if (Input.GetMouseButtonDown(0) && canMove)
        {
            //Get mouse direction and move towards that point
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDirection = mousePosition - (Vector2)transform.position;
            rb.velocity = mouseDirection.normalized * movementSpeed;

            //Start countdown of when player can move again
            moveDelay -= Time.deltaTime;
            StopMovement();
        }

    }

    //Stops the player movement
    private void StopMovement()
    {
        canMove = false;
        StartCoroutine(MoveDelay(moveDelay));
    }

    //Allows the player to move again after delay of X seconds.
    IEnumerator MoveDelay(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
        moveDelay = moveDelayReset;
        yield return null;

    }

}

//void Rotate(float rotateDirection)
//{
//    var rotation = Quaternion.AngleAxis(rotateDirection * rotateAngle * Time.deltaTime, Vector2.up);
//    transform.forward = rotation * transform.forward;
//}


