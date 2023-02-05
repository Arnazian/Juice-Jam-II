using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    [SerializeField] private List<AudioClip> meleeSounds;
    [SerializeField] private List<AudioClip> meleeHitSounds;
    [SerializeField] private float staggerToEnemy;
    [SerializeField] private float damageToEnemy;
    [SerializeField] private float rageAmount;
    [SerializeField] private float meleeRageBuildUp;
    [SerializeField] private float throwEnemyForce;
    private List<GameObject> hitEnemies = new List<GameObject>();
    Camera _camera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _camera = Camera.main;
        if(collision.CompareTag("Enemy"))
        {
            if(!hitEnemies.Contains(collision.gameObject))
                hitEnemies.Add(collision.gameObject);
            // transform.GetComponent<IDamageable>().Damage(damage);
        }
    }

    public void DamageEnemies()
    {
        foreach(GameObject go in hitEnemies)
        {
            //Camera.main.GetComponent<ScreenShake>().DoScreenShake(0.15f, 0.6f);
            go.GetComponent<IDamageable>().Damage(damageToEnemy);
            if (go.GetComponent<EnemyStagger>() != null)
            {
                StaggerKnockback(go.GetComponent<EnemyStagger>());
            }
            FindObjectOfType<VampireFinisher>().IncreaseRage(rageAmount);
            var rand = Random.Range(0, meleeHitSounds.Count);
            AudioManager.Instance.PlaySfx($"playerMeleeHitSfx{rand}", meleeHitSounds[rand], 0, 0.2f, false, false);
            if (go.GetComponent<Rigidbody2D>() != null)
                go.GetComponent<Rigidbody2D>().velocity = -transform.up * throwEnemyForce;
           
            _camera.GetComponent<ScreenShake>().DoScreenShake(0.25f, 0.5f);
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

    public void PlaySfx()
    {
        var rand = Random.Range(0, meleeSounds.Count);
        AudioManager.Instance.PlaySfx($"playerMeleeSfx{rand}", meleeSounds[rand], 1f, 0.375f, false);
    }
}
