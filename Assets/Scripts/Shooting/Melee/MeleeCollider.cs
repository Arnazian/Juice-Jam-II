using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    [SerializeField] private float staggerToEnemy;
    [SerializeField] private float damageToEnemy;
    [SerializeField] private float meleeRageBuildUp;
    [SerializeField] private float throwEnemyForce;
    private List<GameObject> hitEnemies = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("Added enemy to hitnemies list");
            if(!hitEnemies.Contains(collision.gameObject))
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
            if (go.GetComponent<Rigidbody2D>() != null)
                go.GetComponent<Rigidbody2D>().velocity = -transform.up * throwEnemyForce;
            if(go.GetComponent<EnemyStagger>() != null)
            {
               StaggerKnockback(go.GetComponent<EnemyStagger>());
            }
        }
        
        hitEnemies.Clear();
    }
    
    private void StaggerKnockback(EnemyStagger es)
    {
        if (es.GetIsStaggered())
        {
            //es.Knockback();
        }
        else
        {
            es.Stagger(staggerToEnemy, true);
        }
    }
    
    public void Clear()
    {
        hitEnemies.Clear();
    }
}
