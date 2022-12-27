using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [Header("----StateMachine----------")]
    private EnemyStateFactory _states;
    private EnemyBaseState _currentState; public EnemyBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    [SerializeField] string _currentStateName; public string CurrentStateName{ get { return _currentStateName; } set { _currentStateName = value; } }

    [Header("----Timer----------")]
    [SerializeField] float _timeToShoot; public float TimeToShoot { get { return _timeToShoot; } set { _timeToShoot = value; } }
    [SerializeField] float _timeBetweenShots; public float TimeBetweenShots { get { return _timeBetweenShots; } set { _timeBetweenShots = value; } }
    [SerializeField] float _timeSpeed; public float TimeSpeed { get { return _timeSpeed; }}

    [Header("----Attack----------")]
    [SerializeField] Vector3 _attackPosition; public Vector3 AttackPosition { get { return _attackPosition; } set { _attackPosition = value; } }
    [SerializeField] float _speed; public float Speed { get { return _speed; } set { _speed = value; } }
    private Vector3 _attackDirection; public Vector3 AttackDirection { get { return _attackDirection; } set { _attackDirection = value; } }

    [Header("----Reference----------")]
    [SerializeField] GameObject _enemyProjectilePrefab; public GameObject EnemyProjectilePrefab { get { return _enemyProjectilePrefab; } }
    [SerializeField] ParticleSystem _enemyExplosionParticle; public ParticleSystem EnemyExplosionParticle { get { return _enemyExplosionParticle; } }

    private void Awake()
    {
        _states = new EnemyStateFactory(this);
        _currentState = _states.ToPosition();
        _currentState.EnterState();
    }

    private void Update()
    {
        _currentState.UpdateState();
        _currentState.CheckStateChange();
    }

    public void Destruction()
    {
        Instantiate(_enemyExplosionParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
