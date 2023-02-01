using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : BaseMovement
{
    [SerializeField] private float distanceFromPlayerToStop;
    
  
    
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
        MoveTowards(playerTransform, distanceFromPlayerToStop);
    }
}
