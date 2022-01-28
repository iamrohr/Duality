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

    Vector2 mousePosition;
    Vector2 mouseDirection;

    Coroutine SlowDownAbort;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        mouseDirection = (Vector2)Input.mousePosition;


        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Move");

            //Get mouse direction
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDirection = mousePosition - (Vector2)transform.position;

            //Add force towards mouse position
            rb.velocity = mouseDirection.normalized * movementSpeed;
            //StartCoroutine(SlowDownForce(forceSlowDown));
        }


        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    //rb.AddForce(Vector2.right * impulseSpeed, ForceMode2D.Impulse);
        //    //SlowDownForce(0.2f);
        //    rb.velocity += (Vector2)transform.right * forcePower * Time.deltaTime;
        //    StartCoroutine(SlowDownForce(forceSlowDown));
        //}
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    //rb.AddForce(Vector2.left * impulseSpeed, ForceMode2D.Impulse);
        //    //SlowDownForce(0.2f);
        //    rb.velocity -= (Vector2)transform.right * forcePower * Time.deltaTime;
        //    StartCoroutine(SlowDownForce(forceSlowDown));
        //}

    }

    IEnumerator SlowDownForce(float timeSlowDown)
    {
        yield return new WaitForSeconds(timeSlowDown);
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;


        //Rotate(Input.GetAxisRaw("Horizontal"));

    }
}

//void Rotate(float rotateDirection)
//{
//    var rotation = Quaternion.AngleAxis(rotateDirection * rotateAngle * Time.deltaTime, Vector2.up);
//    transform.forward = rotation * transform.forward;
//}


