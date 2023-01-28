using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsCursor : MonoBehaviour
{
    private Vector2 rotationDirection;

    void Update()
    {
        rotationDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float rotationZ = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg-90;

        transform.rotation = Quaternion.Euler(0f, 0f , rotationZ);
    }
}
