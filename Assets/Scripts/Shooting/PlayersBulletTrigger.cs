using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersBulletTrigger : MonoBehaviour
{
    [SerializeField] private GameObject collisionParticle;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            //Instantiate(collisionParticle, transform.position, )

            Destroy(gameObject);
        }
    }
}
