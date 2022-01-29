using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private float moveForce;
    [SerializeField] private float shrinkTime = 0.1f;
    [SerializeField] private GameObject fill;
    [SerializeField] private SpriteRenderer frame;

    private bool _chasing, _absorbing;
    private Rigidbody2D _rb;
    private Transform _transform;
    private Transform _playerTransform;
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
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
        if (_chasing || frame.isVisible) return;
        Die();
    }

    private void FixedUpdate()
    {
        if (_absorbing) return;
        _rb.AddForce(_transform.up * (moveForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
        if (_chasing)
        {
            RotateTowardsPlayer();
        }
    }

    public void Absorb(Transform absorber)
    {
        _absorbing = true;
        _rb.isKinematic = true;
        //_rb.angularVelocity = 0;
        _transform.SetParent(absorber);
        var colliders = GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            col.enabled = false;
        }
        StartCoroutine(ShrinkThenDie(absorber));
    }

    private IEnumerator ShrinkThenDie(Transform absorber)
    {
        var velocity = (absorber.position - _transform.position).normalized;
        float t = 0;
        while (t < 1)
        {
            //_rb.velocity = velocity;
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
            t += Time.deltaTime / shrinkTime;
            yield return null;
        }
        Die();
        yield return null;
    }

    public void Bounce(Vector2 velocity)
    {
        _chasing = false;
        fill.SetActive(true);
        _boxCollider2D.enabled = false;
        _transform.eulerAngles += 180 * Vector3.forward;
        _rb.angularVelocity = 0;
        _rb.AddForce(velocity * 3, ForceMode2D.Impulse);
    }

    private void Die()
    {
        EnemyManager.Instance.RemoveEnemyFromList(gameObject);
        Destroy(gameObject);
    }

    private void RotateTowardsPlayer()
    {
        var heading = _transform.position - _playerTransform.position;
        var rotateAmount = Vector3.Cross(heading.normalized, _transform.up).z;
        _rb.angularVelocity = rotateAmount * rotateSpeed;
    }
}