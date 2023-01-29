using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    [SerializeField] private float damageToEnemy;
    [SerializeField] private float throwEnemyForce;
    private List<GameObject> hitEnemies = new List<GameObject>();
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("Added enemy to hitnemies list");
            hitEnemies.Add(collision.gameObject);
            // transform.GetComponent<IDamageable>().Damage(damage);
        }
    }

    public void DamageEnemies()
    {
        Debug.Log("Damaged enemiess");
        foreach(GameObject go in hitEnemies)
        {
            go.GetComponent<IDamageable>().Damage(damageToEnemy);
            go.GetComponent<IAstarAI>().Move(-transform.up * throwEnemyForce);
        }

        hitEnemies.Clear();
    }
}
