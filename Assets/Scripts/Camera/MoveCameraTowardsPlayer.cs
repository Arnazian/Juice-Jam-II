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

        targetPosition = 
            AVGOfTwoVectors(player.transform.position, AVGOfTwoVectors(player.transform.position, AVGOfTwoVectors(player.transform.position, mousePosition)));

        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, targetPosition.y, -10f), cameraSpeed * screenSizeFloat);
    }

    public static Vector3 AVGOfTwoVectors(Vector3 pos1, Vector3 pos2)
    {
        return (pos1 + pos2) / 2;
    }
}
