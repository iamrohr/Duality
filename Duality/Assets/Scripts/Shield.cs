using System.Linq;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Rigidbody2D holderRB;
    [SerializeField] private PlayerGrow playerGrow;
    
    public int numberOfPoints;
    public float radius = 5;
    
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
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            StartCoroutine(playerGrow.DecreaseSize(1.05f));
            col.GetComponent<Enemy>().Bounce(holderRB.velocity);
        }
    }
    
    private void Setup()
    {
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
    
    public void SetSize(float size)
    {
        size = Mathf.Clamp(size, 0, 1);

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
