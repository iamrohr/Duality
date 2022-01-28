using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private float moveForce;
    

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

    private void FixedUpdate()
    {
        //_rb.velocity = _transform.right * moveSpeed;
        _rb.AddForce(_transform.right * moveForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        
        RotateTowardsPlayer();
    }

    private void RotateTowardsPlayer()
    {
        var heading = _transform.position - _playerTransform.position;

        var rotateAmount = Vector3.Cross(heading.normalized, _transform.right).z;

        _rb.angularVelocity = rotateAmount * rotateSpeed;
    }
}