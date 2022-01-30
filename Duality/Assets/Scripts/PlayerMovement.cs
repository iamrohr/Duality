using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Dash mot musen och rotera sk�lden med A och D

    Rigidbody2D rb;
        
    [Header("Sound")]
    public AudioClip music;
    public float musicVolume = 1f;

    public AudioClip[] playerMovementSound;
    public float playerMovementVolume = 0.25f;

    public float movementSpeed = 10f;
    public float forceSlowDown = 0.2f;

    public float moveDelay = 0.26f;
    float moveDelayReset;
    public bool canMove = true;

    Vector2 mousePosition;
    Vector2 mouseDirection;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDelayReset = moveDelay;

        MusicSound();

}

    // Update is called once per frame
    void Update()
    {
        mouseDirection = (Vector2)Input.mousePosition;

        //Movement
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            PlayerMovementSound();
            //Get mouse direction and move towards that point
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDirection = mousePosition - (Vector2)transform.position;
            rb.velocity = mouseDirection.normalized * movementSpeed;

            //Start countdown of when player can move again

            StopMovement();
        }

        if (Input.GetKey("right") && canMove)
        {
            PlayerMovementSound();
            rb.velocity = Vector3.right * movementSpeed;

            //Start countdown of when player can move again
 
            StopMovement();
        }

        if (Input.GetKey("left") && canMove)
        {
            PlayerMovementSound();
            rb.velocity = Vector3.left * movementSpeed;

            //Start countdown of when player can move again
 
            StopMovement();
        }

        if (Input.GetKey("up") && canMove)
        {
            PlayerMovementSound();
            rb.velocity = Vector3.up * movementSpeed;

            //Start countdown of when player can move again

            StopMovement();
        }

        if (Input.GetKey("down") && canMove)
        {
            PlayerMovementSound();
            rb.velocity = Vector3.down * movementSpeed;

            //Start countdown of when player can move again
            StopMovement();
        }
    }

    //Stops the player movement
    private void StopMovement()
    {
        //moveDelay -= Time.deltaTime;
        canMove = false;
        StartCoroutine(MoveDelay(moveDelay));
    }

    //Allows the player to move again after delay of X seconds.
    IEnumerator MoveDelay(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
        //moveDelay = moveDelayReset;
        yield return null;

    }

    void PlayerMovementSound()
    {
        AudioManager.Instance.sfxAudioSource.PlayOneShot(playerMovementSound[Random.Range(0, playerMovementSound.Length)], playerMovementVolume * PlayerPrefs.GetFloat("masterVolume"));
    }

    void MusicSound()
    {
        AudioManager.Instance.musicAudioSource.PlayOneShot(music, playerMovementVolume * PlayerPrefs.GetFloat("masterVolume"));
    }

    //N�r jag klickar p� A rotera �t v�nster 45 grader med farten 2 m/s och ease in p� slutet.




}

//void Rotate(float rotateDirection)
//{
//    var rotation = Quaternion.AngleAxis(rotateDirection * rotateAngle * Time.deltaTime, Vector2.up);
//    transform.forward = rotation * transform.forward;
//}


