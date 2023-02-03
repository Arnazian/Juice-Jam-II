using UnityEngine;

public class BloodPot : MonoBehaviour
{
    [SerializeField] private float healthToGive = 50;

    private PlayersHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayersHealth>();
    }

    public void Heal()
    {
        _playerHealth.Heal(healthToGive);
    }
}
