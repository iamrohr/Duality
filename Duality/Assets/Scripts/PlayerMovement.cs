using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Sound")] public AudioClip music;
    public float musicVolume = 1f;

    public AudioClip[] playerMovementSound;
    public float playerMovementVolume = 0.25f;

    [Header("Movement Attributes")] public float movementSpeed = 10f;
    public float forceSlowDown = 0.2f;
    public float moveDelay = 0.26f;
    private float _moveDelayReset;
    [HideInInspector]public bool canMove = true;
    
    Rigidbody2D rb;
    private Vector2 _mousePosition;
    private Vector2 _mouseDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _moveDelayReset = moveDelay;

        MusicSound();

        void Update()
        {
            //Mouse Movement
            _mouseDirection = (Vector2) Input.mousePosition;

            if (Input.GetMouseButtonDown(0) && canMove)
            {
                PlayerMovementSound();
                //Get mouse direction and move towards that point
                _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _mouseDirection = _mousePosition - (Vector2) transform.position;
                rb.velocity = _mouseDirection.normalized * movementSpeed;

                //Start countdown of when player can move again

                StopMovement();
            }

            //Keyboard Movement 
            if (Input.GetKey("right") && canMove)
            {
                PlayerMovementSound();
                rb.velocity = Vector3.right * movementSpeed;
                StopMovement();
            }

            if (Input.GetKey("left") && canMove)
            {
                PlayerMovementSound();
                rb.velocity = Vector3.left * movementSpeed;
                StopMovement();
            }

            if (Input.GetKey("up") && canMove)
            {
                PlayerMovementSound();
                rb.velocity = Vector3.up * movementSpeed;
                StopMovement();
            }

            if (Input.GetKey("down") && canMove)
            {
                PlayerMovementSound();
                rb.velocity = Vector3.down * movementSpeed;
                StopMovement();
            }
        }

        //Stops the player movement
        void StopMovement()
        {
            canMove = false;
            StartCoroutine(MoveDelay(moveDelay));
        }

        IEnumerator MoveDelay(float time)
        {
            yield return new WaitForSeconds(time);
            canMove = true;
            yield return null;
        }

        //Sounds
        void PlayerMovementSound()
        {
            AudioManager.Instance.sfxAudioSource.PlayOneShot(
                playerMovementSound[Random.Range(0, playerMovementSound.Length)], playerMovementVolume);
        }

        void MusicSound()
        {
            AudioManager.Instance.musicAudioSource.PlayOneShot(music, playerMovementVolume);
        }
    }
}