using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraTowardsPlayer : MonoBehaviour
{
    public float cameraSpeed = 0.015f;
    private GameObject player;
    private Vector3 targetPosition;


    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        //To make camera move faster on bigger screen because there it has longer distance to go
        float screenSizeFloat = (Screen.width+Screen.height) / 2;

        //making camera a bit towards cursor

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        targetPosition =  (player.transform.position +
            ((player.transform.position + 
            ((player.transform.position + mousePosition) / 2)) / 2)) / 2;


        Debug.DrawLine(targetPosition, targetPosition + Vector3.one/5, Color.red, 1000000);

        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, targetPosition.y, -10f), cameraSpeed * screenSizeFloat);
    }
}
