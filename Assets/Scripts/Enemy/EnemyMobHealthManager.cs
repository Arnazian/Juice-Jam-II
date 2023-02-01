using UnityEngine;

public class EnemyMobHealthManager : HealthManager
{
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private GameObject deathMarker;
    
    [SerializeField] private GameObject myHealthBar;

    private VampireFinisher vampireFinisher;

    protected override void Awake()
    {
        vampireFinisher = FindObjectOfType<VampireFinisher>();
        var newHealthBar = Instantiate(myHealthBar, transform.position, Quaternion.identity);
        newHealthBar.GetComponent<FollowOtherObject>().SetObjectToFollow(gameObject);
        healthBarFill = newHealthBar.GetComponent<FollowOtherObject>().GetHealthImageFill();
        base.Awake();
    }

    public override void Damage(float amount)
    {
        base.Damage(amount);
        vampireFinisher.IncreaseRage(amount);
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
    
    protected override void UpdateHealth()
    {
        base.UpdateHealth();
        if(currentHealth <= 0 )
            RunEnemyDeath();
    }
}
