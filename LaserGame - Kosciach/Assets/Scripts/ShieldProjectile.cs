using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProjectile : MonoBehaviour
{
    [Header("----References-------------")]
    [SerializeField] Camera _camera;
    [SerializeField] ParticleSystem _shieldProjectileParticle;
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] ScoreScript _scoreScript;
    [SerializeField] LayerMask _enemyMask;

    [Header("----Values-------------")]
    [SerializeField] float _speed;
    [SerializeField] float _explosionRadius;

    private Vector2 _halfScreenSize;

    #region MonoBehaviour
    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _camera = FindObjectOfType<Camera>();
        _scoreScript = FindObjectOfType<ScoreScript>();
    }
    private void Start()
    {
        _rigidbody2D.AddForce(transform.up * _speed, ForceMode2D.Impulse);
        _halfScreenSize = new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);
    }
    private void Update()
    {
        CheckWallCollision();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            Destruction();
        }
    }
    #endregion

    #region Methods
    private void Destruction()
    {
        Instantiate(_shieldProjectileParticle, transform.position, transform.rotation);

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, _enemyMask);
        _scoreScript.UpdateScore(enemiesInRange.Length);
        foreach (Collider2D enemy in enemiesInRange)
            Destroy(enemy.gameObject);

        Destroy(gameObject);
    }

    private void CheckWallCollision()
    {
        if(transform.position.x < -_halfScreenSize.x || transform.position.x > _halfScreenSize.x || transform.position.y < -_halfScreenSize.y || transform.position.y > _halfScreenSize.y)
        {
            Destruction();
        }
    }
    #endregion
}
