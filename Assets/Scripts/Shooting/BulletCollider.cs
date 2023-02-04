using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum OwnerOfBulletType
{
    Enemy,
    Player
}

public class BulletCollider : MonoBehaviour
{
    [SerializeField] private List<AudioClip> shootSounds;
    [SerializeField] private List<AudioClip> hitSounds;
    [SerializeField] private GameObject collisionParticle;
    [SerializeField] private OwnerOfBulletType ownerOfBulletType;

    public float staggerAmount;
    public float damage;

    private void Awake()
    {
        var rand = Random.Range(0, shootSounds.Count);
        AudioManager.Instance.PlaySfx($"playerShootSfx{rand}", shootSounds[rand], 1.75f, 0.15f, false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerCollision(other);

        }
        else if(other.CompareTag("Enemy"))
        {
            EnemyCollision(other);
        }
        else if(other.CompareTag("Wall"))
        {
            WallCollision(other);
        }
        else if (other.CompareTag("Bloodpot"))
        {
            BloodpotCollision(other);
        }
        else if (other.GetComponent<BulletCollider>() != null)
        {
            BulletCollision(other);
        }
    }

    private void BloodpotCollision(Collider2D other)
    {
        Instantiate(collisionParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
        if(ownerOfBulletType != OwnerOfBulletType.Player)
            return;
        other.GetComponent<BloodPot>().Heal();
        Destroy(other.gameObject);
    }

    void EnemyCollision(Collider2D other)
    {
        if(ownerOfBulletType != OwnerOfBulletType.Player) 
            return;
        if(other.gameObject.GetComponent<EnemyStagger>() != null) 
        {
            other.gameObject.GetComponent<EnemyStagger>().Stagger(staggerAmount, false);
        }

        var rand = Random.Range(0, hitSounds.Count);
        AudioManager.Instance.PlaySfx($"enemyHitSfx{rand}", hitSounds[rand], 3, 0.5f, false);
        other.transform.GetComponent<IDamageable>().Damage(damage);
        Instantiate(collisionParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void PlayerCollision(Collider2D other)
    {
        if (ownerOfBulletType != OwnerOfBulletType.Enemy)
            return;
        other.transform.GetComponent<IDamageable>().Damage(damage * WaveManager.Instance.GetCurrentDifficultySetting.damageMultiplier);
        Instantiate(collisionParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    void WallCollision(Collider2D other)
    {
        Instantiate(collisionParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    void BulletCollision(Collider2D other)
    {

    }
}
