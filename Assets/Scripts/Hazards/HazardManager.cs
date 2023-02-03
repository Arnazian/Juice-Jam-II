using System.Collections.Generic;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> hazardsPerRound;
    
    private void Start()
    {
        WaveManager.Instance.onRoundStart += SwitchHazards;
        SwitchHazards(1);
    }
    
    private void OnDisable()
    {
        WaveManager.Instance.onRoundStart -= SwitchHazards;
    }

    private void SwitchHazards(int currentRound)
    {
        foreach (var hazard in hazardsPerRound)
            hazard.SetActive(false);
        hazardsPerRound[currentRound - 1].SetActive(true);
    }
}
