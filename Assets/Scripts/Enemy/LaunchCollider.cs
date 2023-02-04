using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchCollider : MonoBehaviour
{
    private EnemyStagger enemyStagger;
    [SerializeField] private GameObject collisionParticle;
    [SerializeField] private float collisionDamage;
    [SerializeField] private float staggerAmount;

    private void Start()
    {
        enemyStagger = GetComponent<EnemyStagger>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!enemyStagger.GetIsKnocked()) { return; }
        if (other.CompareTag("Enemy"))
        {
            EnemyCollision(other);
        }
        else if (other.CompareTag("Wall"))
        {
            WallCollision(other);
        }
    }

    void EnemyCollision(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemyStagger>() != null)
        {
            other.gameObject.GetComponent<EnemyStagger>().Stagger(staggerAmount, false);
        }
        other.transform.GetComponent<IDamageable>().Damage(collisionDamage * 0.75f);
        GetComponent<IDamageable>().Damage(collisionDamage * 0.75f);
        Instantiate(collisionParticle, transform.position, Quaternion.identity);
        enemyStagger.HitSomethingWhileFlying();
    }

    void WallCollision(Collider2D other)
    {
        Instantiate(collisionParticle, transform.position, Quaternion.identity);
        enemyStagger.HitSomethingWhileFlying();
        GetComponent<IDamageable>().Damage(collisionDamage);
    }
}

