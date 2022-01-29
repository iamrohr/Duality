using System;
using System.Linq;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int numberOfPoints;
    public float radius = 5;
    
    private LineRenderer _lineRenderer;
    private EdgeCollider2D _edgeCollider2D;
    private Vector3[] _points;
    private Vector2[] _points2;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _edgeCollider2D = GetComponent<EdgeCollider2D>();
    }

    private void Start()
    {
        _points = new Vector3[numberOfPoints];
        _points2 = new Vector2[numberOfPoints];
        Setup();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().Bounce();
        }
    }

    [ContextMenu("SetSize")]
    public void Test()
    {
        SetSize(0.5f);
    }
    
    public void SetSize(float size)
    {
        size = Mathf.Clamp(size, 0, 1);

        int newNumberOfPoints = Mathf.FloorToInt((numberOfPoints * size));

        var newPoints = new Vector3[newNumberOfPoints];
        var newPoints2 = new Vector2[newNumberOfPoints];

        for (int i = 0; i < newNumberOfPoints; i++)
        {
            newPoints[i] = _points[i];
            newPoints2[i] = _points2[i];
        }

        _lineRenderer.positionCount = newNumberOfPoints;
        _lineRenderer.SetPositions(newPoints);
        _edgeCollider2D.SetPoints(newPoints2.ToList());
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        foreach (var point in _points)
        {
            Gizmos.DrawSphere(point, 0.2f);
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
            _points2[i] = new Vector2(x, y);
            radian += Mathf.PI * 2 / numberOfPoints;
        }
        
        _lineRenderer.positionCount = numberOfPoints;
        _lineRenderer.SetPositions(_points);
        _edgeCollider2D.SetPoints(_points2.ToList());
    }
}
