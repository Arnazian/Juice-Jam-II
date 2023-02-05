using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Slider bossHealthBar;
    public Slider GetBossHealthBar => bossHealthBar;
    
    [SerializeField] private Slider playerHealthBarFill;
    public Slider GetPlayerHealthBarFill => playerHealthBarFill;

    [SerializeField] private Slider rageMeterFill;
    public Slider GetRageMeterFill => rageMeterFill;

    public GameObject gameOverScreen;
    public GameObject GetGameOverScreen => gameOverScreen;
}
