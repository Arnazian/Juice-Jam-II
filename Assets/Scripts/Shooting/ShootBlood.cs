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
    [SerializeField] private ParticleSystem handParticles;
    private float shootCooldown;
    private PlayerActionManager playerActionManager;

    private PlayersHealth playerHealth;
    
    private Animator anim;

    private bool _isShooting;
    private static readonly int IsFiring = Animator.StringToHash("IsFiring");
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        playerActionManager = GetComponent<PlayerActionManager>();
        anim = GetComponent<Animator>();
        shootCooldown = startShootCooldown;
        playerHealth = GetComponent<PlayersHealth>();
    }

    void Update()
    {
        HandleShooting();
    }

    void HandleShooting()
    {
        if (playerHealth.GetHealth <= damageToSelf) { return; }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            handParticles.Stop();
            _isShooting = false;
            anim.SetBool(IsFiring, _isShooting);
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (playerActionManager.CheckIfInAction()) { return; }
            handParticles.Play();
            _isShooting = true;
            anim.SetBool(IsFiring, _isShooting);
        }

        if(_isShooting && shootCooldown <= 0)
        {            
            Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition) - shootSpawn.transform.position, projectileSpeed);
            shootCooldown = startShootCooldown;
        }

        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        /*
        if (Input.GetKey(KeyCode.Mouse0) && shootCooldown <= 0)
        {
            if (playerActionManager.CheckIfInAction()) { return; }

            Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition) - shootSpawn.transform.position, projectileSpeed);
            shootCooldown = startShootCooldown;
            _isShooting = true;
            anim.SetBool(IsFiring, _isShooting);            
        }
        */

    }

    public void Shoot(Vector3 direction, float speed)
    {
        GetComponent<IDamageable>().Damage(damageToSelf);

        _camera.GetComponent<ScreenShake>().DoScreenShake(0.2f, 0.12f);

        var projectileRotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90;
        var projectileRotation =  Quaternion.Euler(0f, 0f, projectileRotationZ);

        var newProjectile = Instantiate(projectile, shootSpawn.transform.position, projectileRotation);
        newProjectile.GetComponent<ProjectileFlyStraight>().speed = speed;
        newProjectile.GetComponent<BulletCollider>().damage = projectileDamage;
    }
}
