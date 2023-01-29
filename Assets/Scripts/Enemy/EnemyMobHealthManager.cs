using UnityEngine;

public class EnemyMobHealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private HealthBar healthBar;

    private void Update()
    {
        if (healthBar.GetHealth <= 0)
        {
            Destroy(gameObject);
            var particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
            particles.Play();
            WaveManager.Instance.OnDeath();
        }
    }
    
    public void Damage(float amount)
    {
        healthBar.Damage(amount);
    }
}
