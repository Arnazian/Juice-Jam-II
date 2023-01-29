using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalomonProjectile : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float projectileSpeed;

    [Header("Destroying Projectile After Time")]
    [SerializeField] private bool destroyAfterTime = false;
    [SerializeField] private float destroyAfterSeconds;

    [SerializeField] private bool doesHitPlayer = false;
    [SerializeField] private bool doesHitEnemy = false;

    private string enemyTag = "Enemy";
    private string playerTag = "Player";


    private Rigidbody2D rb;

    void Start()
    {
        if (destroyAfterTime) { StartCoroutine(DestroyObjectAfterSeconds(destroyAfterSeconds)); }
    }

    void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.position += transform.up * Time.deltaTime * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag) && doesHitEnemy)
        {
            print("HIT ENEMY");
        }
        if (collision.CompareTag(playerTag) && doesHitPlayer)
        {
            print("HIT PLAYER");
        }
    }
    IEnumerator DestroyObjectAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
