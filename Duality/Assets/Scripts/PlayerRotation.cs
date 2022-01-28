using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    bool rotating;
    float rotateTime = 0.2f;
    float degreesToRotate = 90f;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A) && !rotating)
        {
            rotating = true;
            StartCoroutine(RotateMe(Vector3.forward * degreesToRotate, rotateTime));
        }
        if (Input.GetKeyDown(KeyCode.D) && !rotating)
        {
            rotating = true;
            StartCoroutine(RotateMe(Vector3.forward * -degreesToRotate, rotateTime));
        }

    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);

        for (var t = 0f; t <= 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);

            yield return null;
        }

        transform.rotation = toAngle;
        rotating = false;
    }
}
