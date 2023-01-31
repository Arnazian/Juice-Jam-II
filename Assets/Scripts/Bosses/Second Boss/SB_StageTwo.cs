using DG.Tweening;
using UnityEngine;

public class SB_StageTwo : Stage2Base
{
    [SerializeField] private Vector2 poolRandomTime = new Vector2(0.5f, 2.5f);
    [SerializeField] private float timeToLaunch = 2f;
    [SerializeField] private GameObject poolMarker;
    [SerializeField] private GameObject pool;

    private Vector3 _projectileTarget;

    private Transform _player;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    public override void StartStageTwo()
    {
        var randomProjectileTime = Random.Range(poolRandomTime.x, poolRandomTime.y);
        Invoke(nameof(ShootProjectile), randomProjectileTime);
    }

    private void ShootProjectile()
    {
        _projectileTarget = _player.position;
        var marker = Instantiate(poolMarker, _projectileTarget, Quaternion.identity);
        marker.transform.DOScale(Vector3.zero, timeToLaunch).OnComplete(() =>
        {
            CreatePool();
            Destroy(marker);
            var randomProjectileTime = Random.Range(poolRandomTime.x, poolRandomTime.y);
            Invoke(nameof(ShootProjectile), randomProjectileTime);
        });
    }

    private void CreatePool()
    {
        var poolObject = Instantiate(pool, _projectileTarget, Quaternion.identity);
    }
}
