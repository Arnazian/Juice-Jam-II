using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootBlood : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject shootCenter, shootSpawn;

    [SerializeField] private int startShootCooldown = 15;
    private int shootCooldown;


    void Start()
    {
        shootCooldown = startShootCooldown;
    }

    void FixedUpdate()
    {
        if (Mouse.current.leftButton.isPressed && shootCooldown <= 0)
        {
            Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition) - shootSpawn.transform.position, projectileSpeed);
            shootCooldown = startShootCooldown;
        }
        else
        {
            shootCooldown--;
        }
    }

    public void Shoot(Vector3 direction, float speed)
    {
        ScreenShake cameraShakeScript = Camera.main.GetComponent<ScreenShake>();
        cameraShakeScript.shakeStrengthModifier = 0.1f;
        cameraShakeScript.duration = 0.25f;
        cameraShakeScript.startShake = true;


        float spread, rand1, rand2;
        spread = 0.15f;
        rand1 = Random.Range(-spread, spread);
        rand2 = Random.Range(-spread, spread);

        float projectileRotationZ = Mathf.Atan2(direction.y+rand1, direction.x+rand2) * Mathf.Rad2Deg-90;

        Quaternion projectileRotation =  Quaternion.Euler(0f, 0f, projectileRotationZ);

        GameObject newProjectile = Instantiate(projectile, shootSpawn.transform.position, projectileRotation);
        newProjectile.GetComponent<ProjectileFlyStraight>().speed = speed;
    }
}
