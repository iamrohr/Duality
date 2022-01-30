using System.Linq;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Rigidbody2D holderRb;
    [SerializeField] private PlayerGrow playerGrow;
    [SerializeField] private int numberOfPoints;
    [SerializeField] private float radius = 5;
    
    private float _size;
    private LineRenderer _lineRenderer;
    private EdgeCollider2D _edgeCollider2D;
    private Vector3[] _points;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _edgeCollider2D = GetComponent<EdgeCollider2D>();
    }

    private void Start()
    {
        _points = new Vector3[numberOfPoints];
        Setup();
        SetSize(0.8f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;
        
        StartCoroutine(playerGrow.ChangeSize(0.95f));
        col.GetComponent<Enemy>().Bounce(holderRb.velocity);
    }
    
    private void Setup()
    {
        _size = 1;
        float radian = 0;
        for (int i = 0; i < numberOfPoints; i++)
        {
            var x = Mathf.Cos(radian) * radius;
            var y = Mathf.Sin(radian) * radius;

            _points[i] = new Vector3(x, y, 0);
            radian += Mathf.PI * 2 / numberOfPoints;
        }
        SetPoints(_points);
    }

    [ContextMenu("SetSize")]
    public void Test()
    {
        SetSize(0.2f);
    }

    public void AddSize(float addend)
    {
        SetSize(_size + addend);
    }

    public void MultiplySize(float multiplier)
    {
        SetSize(_size * multiplier);
    }

    public void SetSize(float size)
    {
        size = Mathf.Clamp(size, 0, 1);
        _size = size;

        int newNumberOfPoints = Mathf.FloorToInt((numberOfPoints * size));
        var indexOffset = (numberOfPoints - newNumberOfPoints) / 2;
        var newPoints = new Vector3[newNumberOfPoints];

        for (int i = 0; i < newNumberOfPoints; i++)
        {
            newPoints[i] = _points[i + indexOffset];
        }
        SetPoints(newPoints);
    }

    private void SetPoints(Vector3[] points)
    {
        var v2PointList = points.Select(point => (Vector2) point).ToList();

        _lineRenderer.positionCount = points.Length;
        _lineRenderer.SetPositions(points);
        _edgeCollider2D.SetPoints(v2PointList);
    }
}
