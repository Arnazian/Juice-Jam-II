using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapDirectionTowardsCursor : MonoBehaviour
{
    private Vector2 rotationDirection;

    void Update()
    {
        if(PauseMenu.Instance.IsPaused)
            return;
        
        rotationDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (rotationDirection.x < 0)
        {
            transform.eulerAngles = RotateY(0);
        }
        else if (rotationDirection.x > 0)
        {
            transform.eulerAngles = RotateY(180);
        }
    }

    Vector3 RotateY(float angle)
    {
        return new Vector3(transform.rotation.x, angle, transform.rotation.z);
    }
}
