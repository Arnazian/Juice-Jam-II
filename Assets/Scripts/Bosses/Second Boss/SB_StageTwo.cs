using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SB_StageTwo : Stage2Base
{
    [SerializeField] private Vector2 projectileRandomTime = new Vector2(0.5f, 2.5f);
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject pool;

    private GameObject _activeProjectile;
    private Vector3 _projectileTarget;

    private Transform _player;

    private FacePlayer _facePlayer;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _facePlayer = GetComponent<FacePlayer>();
    }

    public override void StartStageTwo()
    {
        var randomProjectileTime = Random.Range(projectileRandomTime.x, projectileRandomTime.y);
        Invoke(nameof(ShootProjectile), randomProjectileTime);
    }

    private void Update()
    {
        if (_activeProjectile != null)
        {
            if (Vector3.Distance(_activeProjectile.transform.position, _projectileTarget) <= 2f)
            {
                Destroy(_activeProjectile);
                _activeProjectile = null;
                CreatePool();
            }
        }
    }

    private void ShootProjectile()
    {
        _facePlayer.DisableFacePlayer();
        var bullet = Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
        _projectileTarget = _player.position;
        _activeProjectile = bullet;
        Invoke(nameof(EnableFacePlayer), 0.15f);
        var randomProjectileTime = Random.Range(projectileRandomTime.x, projectileRandomTime.y);
        Invoke(nameof(ShootProjectile), randomProjectileTime);
    }

    private void EnableFacePlayer()
    {
        _facePlayer.EnableFacePlayer();
    }
    
    private void CreatePool()
    {
        Debug.Log("created pool");
    }
}
