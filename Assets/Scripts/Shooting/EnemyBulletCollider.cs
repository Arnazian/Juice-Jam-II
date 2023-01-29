using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBulletCollider : MonoBehaviour
{
    [HideInInspector] public float damage = 100;
    
    [SerializeField] private GameObject collisionParticle;

    void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 normal = other.contacts[0].normal;
        Vector2 point = other.contacts[0].point;

        Quaternion particleRotation = Quaternion.LookRotation(normal);

        Debug.DrawRay(other.contacts[0].point, other.contacts[0].normal, Color.red, 200000f);

        Instantiate(collisionParticle, point, particleRotation);
        
        if(other.transform.CompareTag("Player"))
            other.transform.GetComponent<IDamageable>().Damage(damage);
        
        Destroy(gameObject);
    }
}
