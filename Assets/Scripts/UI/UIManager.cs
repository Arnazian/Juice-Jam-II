using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private HealthBar bossHealthBar;
    public HealthBar GetBossHealthBar => bossHealthBar; 
    
    [SerializeField] private HealthBar playerHealthBar;
    public HealthBar GetPlayerHealthBar => playerHealthBar;
}
