using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Slider bossHealthBar;
    public Slider GetBossHealthBar => bossHealthBar; 
    
    [SerializeField] private Slider playerHealthBar;
    public Slider GetPlayerHealthBar => playerHealthBar;

    [SerializeField] private Slider rageMeter;
    public Slider GetRageMeter => rageMeter;
}
