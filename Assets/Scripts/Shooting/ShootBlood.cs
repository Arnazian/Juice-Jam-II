using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootBlood : MonoBehaviour
{
    [SerializeField] private float projectileDamage = 5f;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject shootSpawn;
    [SerializeField] private float damageToSelf;
    [SerializeField] private float startShootCooldown = 1;
    private float shootCooldown;
    private PlayerActionManager playerActionManager;

    private PlayersHealth playerHealth;

    void Start()
    {
        playerActionManager = GetComponent<PlayerActionManager>();
        shootCooldown = startShootCooldown;
        playerHealth = GetComponent<PlayersHealth>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0) && shootCooldown <= 0)
        {
            if(playerActionManager.CheckIfInAction()) { return; }
            Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition) - shootSpawn.transform.position, projectileSpeed);
            shootCooldown = startShootCooldown;
        }
        else
        {
            shootCooldown-=Time.deltaTime;
        }
    }

    public void Shoot(Vector3 direction, float speed)
    {
        GetComponent<IDamageable>().Damage(damageToSelf);

        Camera.main.GetComponent<ScreenShake>().DoScreenShake(0.15f, 0.12f);

        var projectileRotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90;

        var projectileRotation =  Quaternion.Euler(0f, 0f, projectileRotationZ);

        var newProjectile = Instantiate(projectile, shootSpawn.transform.position, projectileRotation);
        newProjectile.GetComponent<ProjectileFlyStraight>().speed = speed;
        newProjectile.GetComponent<BulletCollider>().damage = projectileDamage;
        // newProjectile.GetComponent<BulletCollider>().damage = projectileDamage + playerHealth.GetMaxHealth() - playerHealth.GetCurHealth();
    }
}
