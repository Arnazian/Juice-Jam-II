using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsCursor : BaseMovement
{
    private Vector2 rotationDirection;

    void Update()
    {
        if (PauseMenu.Instance.IsPaused) { return; }

        rotationDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        RotateTowards(rotationDirection);
    }
}
