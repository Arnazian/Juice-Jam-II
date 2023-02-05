using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyMobHealthManager : HealthManager
{
    [SerializeField] private ParticleSystem hitParticles;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private GameObject deathMarker;
    [SerializeField] private GameObject[] deathSplatter;
    [SerializeField] private GameObject getHitSplatter;

    [SerializeField] private GameObject myHealthBar;

    private Slider staggerBarFill;

    private VampireFinisher vampireFinisher;
    private Material startMaterial;


    protected override void Start()
    {
        startMaterial = transform.Find("Sprite").GetComponent<SpriteRenderer>().material;
        vampireFinisher = FindObjectOfType<VampireFinisher>();
        var newHealthBar = Instantiate(myHealthBar, transform.position, Quaternion.identity);
        newHealthBar.GetComponent<FollowOtherObject>().SetObjectToFollow(gameObject);
        healthBarFill = newHealthBar.GetComponent<FollowOtherObject>().GetHealthImageFill();
        healthBarFill.maxValue = maxHealth;
        healthBarFill.value = currentHealth;
        staggerBarFill = newHealthBar.GetComponent<FollowOtherObject>().GetStaggerImageFill();
        

        base.Start();
    }

    public override void Damage(float amount)
    {
        StartCoroutine("FlashWhite", 0.05f);

        if(vampireFinisher.GetSuckingBlood())
            return;

        float rageAdjustedDamage = vampireFinisher.GetRageAdjustedDamage(amount);
        float difficultyAdjustedDamage = rageAdjustedDamage * WaveManager.Instance.GetCurrentDifficultySetting.damageMultiplier;

        base.Damage(difficultyAdjustedDamage);
        Instantiate(getHitSplatter, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        var particles = Instantiate(hitParticles, transform.position, Quaternion.identity);
        particles.Play();
    }

    [SerializeField] private Material whiteColorMaterial;
    IEnumerator FlashWhite(float time)
    {
        transform.Find("Sprite").GetComponent<SpriteRenderer>().material = whiteColorMaterial;

        yield return new WaitForSeconds(time);

        transform.Find("Sprite").GetComponent<SpriteRenderer>().material = startMaterial;
    }

    public void DamageWhileBloodSuck(float amount)
    {
        base.Damage(amount * WaveManager.Instance.GetCurrentDifficultySetting.damageMultiplier);
    }

    public void RunEnemyDeath()
    {
        Camera _camera = Camera.main;
        _camera.GetComponent<ScreenShake>().DoScreenShake(0.4f, 0.75f);
        int i = Random.Range(0, deathSplatter.Length - 1);
        Instantiate(deathSplatter[i], transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));        
        var particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        particles.Play();
        WaveManager.Instance.EnemyMobDeath();
        Destroy(gameObject);
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


    public Slider GetStaggerBar() { return staggerBarFill; }
}
