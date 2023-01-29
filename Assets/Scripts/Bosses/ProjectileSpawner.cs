using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private bool isShooting = false;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;

    private float shootIntervalMax;
    private float shootIntervalCur;
    void Start()
    {
        shootIntervalCur = 0;
    }

   
    void Update()
    {
        ContiniousShooting();
    }

    public void EnableShooting()
    {
        isShooting = true;
    }
    public void DisableShooting()
    {
        isShooting = false;
    }
    void ContiniousShooting()
    {
        if (!isShooting) { return;  }

        if(shootIntervalCur <= 0)
        {
            shootIntervalCur = shootIntervalMax;
            DoShoot();
        }
        shootIntervalCur -= Time.deltaTime;
        
    }

    public void SetShootInterval(float shootInterval)
    {
        shootIntervalMax = shootInterval;
    }
    void DoShoot()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }
}
