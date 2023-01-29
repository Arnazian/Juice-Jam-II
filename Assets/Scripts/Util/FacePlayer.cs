using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{    
    [SerializeField] private GameObject objectToFacePlayer;
    [Range(0f, 100f)]
    [SerializeField] private float rotateSpeed;

    private GameObject player;
    private string playerTag = "Player";
    private bool canFacePlayer = true;


    void Start()
    {
        //replace
        player = GameObject.FindGameObjectWithTag(playerTag);
    }

    void Update()
    {
        if (canFacePlayer)
        {
            DoFacePlayer();
        }
    }

    void DoFacePlayer()
    {
        Vector2 direction;
        direction.x = player.transform.position.x - transform.position.x;
        direction.y = player.transform.position.y - transform.position.y;
        Vector3 lerpVar = Vector3.Lerp(transform.up, direction, Time.deltaTime * rotateSpeed);
        objectToFacePlayer.transform.up = lerpVar;
    }

    public void EnableFacePlayer()
    {
        canFacePlayer = true;
    }
    public void DisableFacePlayer()
    {
        canFacePlayer = false;
    }
}