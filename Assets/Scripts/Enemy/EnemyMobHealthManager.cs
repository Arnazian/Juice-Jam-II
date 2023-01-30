using UnityEngine;

public class EnemyMobHealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject deathMarker;

    private void Update()
    {
        if (healthBar.GetHealth <= 0)
        {
            DestroyEnemy();
        }
    }
    
    public void Damage(float amount)
    {
        healthBar.Damage(amount);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
        var particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        particles.Play();
        WaveManager.Instance.OnDeath();
    }
    public void SetDeathMarker(bool newStatus)
    {
        deathMarker.SetActive(newStatus);
    }
}
