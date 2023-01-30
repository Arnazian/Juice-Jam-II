using UnityEngine;
using UnityEngine.UI;

public class PlayersHealth : MonoBehaviour, IDamageable
{
    
    [SerializeField]private float playerHealthMax;
    private float playerHealthCur;

    private Slider playerHealthSlider;
    private bool isImmune = false;

    void Start()
    {
        playerHealthSlider = UIManager.Instance.GetPlayerHealthBar;
        playerHealthCur = playerHealthMax;
        playerHealthSlider.maxValue = playerHealthCur;
        playerHealthSlider.value = playerHealthCur;
        
    }

    void FixedUpdate()
    {
        
    }


    public void Damage(float amount)
    {
        if (isImmune) { return; }
        if (GetComponent<Dash>().isDashing) { return; }

        playerHealthCur -= amount;
        UpdateHealthValue();
    }

    public void Heal(float amount)
    {
        playerHealthCur += amount;
        UpdateHealthValue();
    }
    public void SetHealth(float newValue)
    {
        playerHealthCur = newValue;
        UpdateHealthValue();
    }

    private void UpdateHealthValue()
    {
        if (playerHealthCur <= 0) { RunPlayerDeath(); }
        if(playerHealthCur > playerHealthMax) { playerHealthCur = playerHealthMax; }
        playerHealthSlider.value = playerHealthCur;
    }

    private void RunPlayerDeath()
    {
        Destroy(gameObject);
    }
    public void SetImmuneStatus(bool newStatus) { isImmune = newStatus; }

    public float GetMaxHealth() => playerHealthMax;
    public float GetCurHealth() => playerHealthCur;
}
