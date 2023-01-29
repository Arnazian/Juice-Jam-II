using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceFromPlayerToStop;
    [SerializeField] private float deaccelerationSpeed;

    private bool isMoving;
    private bool canMove = true;

    private Rigidbody2D rb;
    private Transform player;
    
    public bool IsMoving => isMoving;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (!canMove) { return; }
        if(Vector3.Distance(transform.position, player.position) <= distanceFromPlayerToStop)
        {
            isMoving = false;
            rb.velocity = Vector3.zero; 
            return;
        }

        isMoving = true;
        rb.velocity = transform.up * moveSpeed;
        //rb.MovePosition(rb.position + ((Vector2)transform.up * moveSpeed * Time.deltaTime));
    }
}
