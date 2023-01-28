using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileFlyStraight : MonoBehaviour
{
    [HideInInspector] public float speed = 10;
    public float toDespawn = 100;
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.rotation*new Vector3(0, speed, 0);

        if(toDespawn<0)
        {
            Destroy(gameObject);
        }
        else
        {
            toDespawn--;
        }
    }
}
