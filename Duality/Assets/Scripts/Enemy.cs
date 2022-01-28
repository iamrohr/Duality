using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private float moveForce;
    [SerializeField] private GameObject fill;
    [SerializeField] private SpriteRenderer frame;

    private bool _chasing;
    private Rigidbody2D _rb;
    private Transform _transform;
    private Transform _playerTransform;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _transform = transform;
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        _chasing = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Bounce();
        }

        if (_chasing || frame.isVisible) return;
        EnemyManager.Instance.RemoveEnemyFromList(gameObject);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_transform.up * (moveForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
        if (_chasing)
        {
            RotateTowardsPlayer();
        }
    }

    public void Bounce()
    {
        _chasing = false;
        fill.SetActive(true);
        _transform.eulerAngles += 180 * Vector3.forward;
        _rb.angularVelocity = 0;
    }

    private void RotateTowardsPlayer()
    {
        var heading = _transform.position - _playerTransform.position;
        var rotateAmount = Vector3.Cross(heading.normalized, _transform.up).z;
        _rb.angularVelocity = rotateAmount * rotateSpeed;
    }
}