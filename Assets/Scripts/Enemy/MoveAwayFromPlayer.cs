using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAwayFromPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float preferredDistance;
    [SerializeField] private float preferredDistanceBuffer;
    [SerializeField] private float deaccelerationSpeed;

    private bool isMoving;
    private bool canMove = true;

    private Rigidbody2D rb;
    private Transform player;



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
        if(Vector3.Distance(transform.position, player.position) > preferredDistance + preferredDistanceBuffer)
        {
            isMoving = true;
            rb.velocity = transform.up * moveSpeed;
        }
        else if(Vector3.Distance(transform.position, player.position) < preferredDistance - preferredDistanceBuffer)
        {
            isMoving = true;
            rb.velocity = -transform.up * moveSpeed;
        }
        else
        {
            isMoving = false;
            rb.velocity = Vector3.zero;
            return;
        }                  
    }

    public bool IsMoving => isMoving;
    public void SetCanMove(bool newValue) { canMove = newValue; }
}
