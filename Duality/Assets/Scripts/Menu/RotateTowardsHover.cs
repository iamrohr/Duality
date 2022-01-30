using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateTowardsHover:MonoBehaviour
{
    RectTransform _rotation;
    Vector3 _getPosition;
    float _targetAngle, _currentAngle, _angle, _rotationSpeed;
    public bool _invert;

    void Start()
    {
        _rotation = GetComponent<RectTransform>();
        _angle = _currentAngle = _targetAngle = 0;
    }

    void Update()
    {
        if(_angle != _targetAngle)
        {
            _rotationSpeed += Time.deltaTime;
            _angle = Mathf.Lerp(_currentAngle, _targetAngle, _rotationSpeed);
        }

        _rotation.rotation = Quaternion.Euler(0, 0, _angle);
    }

    public void GetAngle(RectTransform mousePosition)
    {
            _currentAngle = _angle;
            _rotationSpeed = 0;
            _getPosition = mousePosition.position - transform.position;
            if(_invert) _getPosition = -_getPosition;
            _targetAngle = Mathf.Atan2(_getPosition.y, _getPosition.x) * Mathf.Rad2Deg;
    }
}
