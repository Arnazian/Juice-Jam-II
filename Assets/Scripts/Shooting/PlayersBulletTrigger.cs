using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersBulletTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
