using DG.Tweening;
using UnityEngine;

public class SB_StageOne : Stage1Base
{
    [Header("Power Attack")]
    [SerializeField] private Vector2 powerAttackRandomTime = new Vector2(7.5f, 15f);
    [SerializeField] private float healthRemaining = 99f;
    [SerializeField] private float damageToStop = 200f;
    [SerializeField] private float chargeTime = 4f;

    [Header("Teleportation")]
    [SerializeField] private Vector2 teleportRandomTime = new Vector2(1f, 2.5f);
    [SerializeField] private float teleportTime = 0.5f;
    [SerializeField] private Vector2 teleportSize;
    
    private BossHealthManager _healthManager;
    private PlayersHealth _playerHealth;

    private bool _canTp = true;
    private bool _isChargingPowerAttack = false;
    private float _powerAttackStartHp;
    
    private bool _stageActive;

    private void Awake()
    {
        _healthManager = GetComponent<BossHealthManager>();
        _playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayersHealth>();
        StartStageOne();
    }

    public override void StartStageOne()
    {
        _stageActive = true;
        _canTp = true;
        MoveToRandomPosition();
        var randPowerAttack = Random.Range(powerAttackRandomTime.x, powerAttackRandomTime.y);
        Invoke(nameof(StartPowerAttack), randPowerAttack);
    }

    private void Update()
    {
        if (_isChargingPowerAttack)
        {
            if (_powerAttackStartHp - _healthManager.GetHealth >= damageToStop)
                StopPowerAttack();
        }

        if (!_stageActive)
        {
            CancelInvoke(nameof(MoveToRandomPosition));
            CancelInvoke(nameof(StartPowerAttack));
            _isChargingPowerAttack = false;
            _canTp = false;
        }
    }

    private void StartPowerAttack()
    {
        if(!_stageActive)
            return;
        _canTp = false;
        _powerAttackStartHp = _healthManager.GetHealth;
        _isChargingPowerAttack = true;
        transform.DOScale(new Vector3(2.5f, 2.5f, 2.5f), chargeTime).OnComplete(() =>
        {
            _isChargingPowerAttack = false;
            _playerHealth.SetHealth(healthRemaining);
            _canTp = true;
            var randTp = Random.Range(teleportRandomTime.x, teleportRandomTime.y);
            Invoke(nameof(MoveToRandomPosition), randTp);
            transform.DOScale(Vector3.one, 0.025f);
        });
        
        var randPowerAttack = Random.Range(7.5f, 15f);
        Invoke(nameof(StartPowerAttack), randPowerAttack);
    }

    private void StopPowerAttack()
    {
        if(!_stageActive)
            return;
        _isChargingPowerAttack = false;
        CancelInvoke(nameof(StartPowerAttack));
        DOTween.Kill(transform);
        transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
        {
            _canTp = true;
            var randTp = Random.Range(teleportRandomTime.x, teleportRandomTime.y);
            Invoke(nameof(MoveToRandomPosition), randTp);
        });
    }
    
    private void MoveToRandomPosition()
    {
        if((!_stageActive) || (!_canTp || transform.localScale != Vector3.one))
            return;
        var randTp = Random.Range(teleportRandomTime.x, teleportRandomTime.y);
        Invoke(nameof(MoveToRandomPosition), randTp);
        var randX = Random.Range(-teleportSize.x, teleportSize.x);
        var randY = Random.Range(-teleportSize.y, teleportSize.y);
        var randomPos = new Vector2(randX, randY);
        MoveToPosition(randomPos);
    }

    private void MoveToPosition(Vector3 position)
    {
        transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), teleportTime).OnComplete(() =>
        {
            transform.position = position;
            transform.localScale = Vector3.one;
        });
    }
    
    public override void StopStageOne()
    {
        _stageActive = false;
        _canTp = false;
        StopPowerAttack();
        MoveToPosition(Vector3.zero);
    }
}
