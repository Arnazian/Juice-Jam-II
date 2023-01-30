using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EnemyMobHealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private GameObject deathMarker;

    private float healthValueCur;
    [SerializeField]private float healthValueMax;
    [SerializeField] private Slider mobHealthBar;

    private void Start()
    {
        healthValueCur = healthValueMax;
        mobHealthBar.maxValue = healthValueCur;
        mobHealthBar.value = healthValueCur;
    }
    private void Update()
    {
    }
    
    public void Damage(float amount)
    {
        healthValueCur -= amount;
        UpdateHealth();
    }
    public void Heal(float amount)
    {
        healthValueCur += amount;
        UpdateHealth();
    }
    public void SetHealth(float amount)
    {
        healthValueCur = amount;
        UpdateHealth();
    }

    public void RunEnemyDeath()
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


    private void UpdateHealth()
    {
        if(healthValueCur <= 0 ) { RunEnemyDeath(); }
        mobHealthBar.value = healthValueCur;
    }
}
