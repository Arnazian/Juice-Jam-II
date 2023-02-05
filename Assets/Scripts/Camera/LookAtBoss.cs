using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtBoss : MonoBehaviour
{
    public float cameraSpeed = 0.015f;
    private GameObject player;
    private Vector3 targetPosition;
    [HideInInspector] public float howLongStayAtBoss = 1;
    private bool isLookingAtBoss;


    void Start()
    {
        isLookingAtBoss = false;
        player=GameObject.FindGameObjectWithTag("Player");
    }

    public IEnumerator DoLookAtBoss(Vector3 bossPosition)
    {
        isLookingAtBoss = true;

        GetComponent<MoveCameraTowardsPlayer>().enabled = false;
        bossPosition = new Vector3(bossPosition.x, bossPosition.y, transform.position.z);
        transform.position = bossPosition;
        targetPosition = bossPosition;
        yield return new WaitForSeconds(howLongStayAtBoss);
        transform.position = targetPosition;
        Time.timeScale = 0;
        PauseMenu.Instance.IsPaused = true;
        GetComponent<MoveCameraTowardsPlayer>().enabled = true;

        isLookingAtBoss = false;
    }

    void FixedUpdate()
    {
        //To make camera move faster on bigger screen because there it has longer distance to go
        float screenSizeFloat = (Screen.width+Screen.height) / 2;

        //making camera a bit towards cursor

        if (isLookingAtBoss)
            transform.position = targetPosition;
    }

    public static Vector3 AVGOfTwoVectors(Vector3 pos1, Vector3 pos2)
    {
        return (pos1 + pos2) / 2;
    }
}
