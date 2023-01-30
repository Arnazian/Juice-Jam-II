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
    [SerializeField] private GameObject myHealthBar;

    private VampireFinisher vampireFinisher;

    private void Start()
    {
        vampireFinisher = FindObjectOfType<VampireFinisher>();
        GameObject newHealthBar = Instantiate(myHealthBar, transform.position, Quaternion.identity);
        newHealthBar.GetComponent<FollowOtherObject>().SetObjectToFollow(gameObject);
        mobHealthBar = newHealthBar.GetComponent<FollowOtherObject>().GetHealthSlider();
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
        vampireFinisher.IncreaseRage(amount);
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
        Debug.Log("ONe enemy killed");
        Destroy(gameObject);
        var particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        particles.Play();
        WaveManager.Instance.EnemyMobDeath();
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
