using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorScript : MonoBehaviour
{
    [Header("----References-------------")]
    [SerializeField] List<Transform> _cores;
    [SerializeField] ParticleSystem _coreExplosionParticle;
    [SerializeField] ScoreScript _scoreScript;
    [SerializeField] LayerMask _enemyMask;

    [Header("----Values-------------")]
    [SerializeField] float _explosionRadius;
    [SerializeField] int _coreCount;

    private bool _isDestroyed = false;

    private void Awake()
    {
        _scoreScript = FindObjectOfType<ScoreScript>();
    }
    private void Start()
    {
        _coreCount = transform.childCount - 1;
        for (int i = 0; i < transform.childCount - 1; i++)
            _cores.Add(transform.GetChild(i));
    }

    public void TakeDamage()
    {
        if (!_isDestroyed)
        {
            _coreCount--;

            UpdateCores();
            if (_coreCount == 0) Destruction();
        }
    }

    private void UpdateCores()
    {
        Destroy(_cores[0].gameObject);
        Instantiate(_coreExplosionParticle, _cores[0].position, Quaternion.identity);

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, _enemyMask);
        foreach (Collider2D enemyInRange in enemiesInRange)
        {
            if (enemyInRange.CompareTag("Enemy"))
            {
                enemyInRange.GetComponent<EnemyStateMachine>().Destruction();
                _scoreScript.UpdateScore(1);
            }
            if (enemyInRange.CompareTag("EnemyProjectile"))
                enemyInRange.GetComponent<EnemyProjectileScript>().Destruction();
        }

        _cores.RemoveAt(0);
    }

    private void Destruction()
    {
        Debug.Log("Game Over!");
        _isDestroyed = true;

        //Play destruction animation
        //When destruction animation ends switch to main menu
    }
}
