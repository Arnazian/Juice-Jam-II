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
            transform.eulerAngles = new Vector3(transform.rotation.x, 0, transform.rotation.z);
        }
        else if (rotationDirection.x > 0)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 180, transform.rotation.z);
        }
    }
}
