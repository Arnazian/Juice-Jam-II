using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAwayFromPlayer : BaseMovement
{
    [SerializeField] private float preferredDistance;
    [SerializeField] private float preferredDistanceBuffer;

    private Transform playerTransform;



    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        StayAway(playerTransform, preferredDistance, preferredDistanceBuffer);                 
    }
}
