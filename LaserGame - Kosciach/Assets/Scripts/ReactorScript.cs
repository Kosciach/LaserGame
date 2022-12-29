using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReactorScript : MonoBehaviour
{
    [Header("----References-------------")]
    [SerializeField] List<Transform> _cores;
    [SerializeField] ParticleSystem _coreExplosionParticle;
    [SerializeField] ParticleSystem _reactorExplosionParticle;
    [SerializeField] ScoreScript _scoreScript;
    [SerializeField] ShakeScript _shakeScript;
    [SerializeField] GameObject _spawner;
    [SerializeField] Canvas _mainCanvas;
    [SerializeField] Image _fadeImage;
    [SerializeField] LayerMask _enemyMask;

    [Header("----Values-------------")]
    [SerializeField] float _explosionRadius;
    [SerializeField] int _coreCount;

    private bool _isDestroyed = false;

    private void Start()
    {
        Transform reactorParts = transform.GetChild(0);
        _coreCount = reactorParts.childCount - 1;
        for (int i = 0; i < reactorParts.childCount - 1; i++)
            _cores.Add(reactorParts.GetChild(i));
    }

    public void TakeDamage()
    {
        if (!_isDestroyed)
        {
            _coreCount--;
            _shakeScript.Shake(4f, 0.5f);

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
        _shakeScript.Shake(10f, 2f);

        _spawner.SetActive(false);
        Collider2D[] enemiesInScene = Physics2D.OverlapCircleAll(transform.position, 100f, _enemyMask);
        foreach (Collider2D enemyInRange in enemiesInScene)
        {
            if (enemyInRange.CompareTag("Enemy"))
                enemyInRange.GetComponent<EnemyStateMachine>().Destruction();
            if (enemyInRange.CompareTag("EnemyProjectile"))
                enemyInRange.GetComponent<EnemyProjectileScript>().Destruction();
        }
        transform.GetChild(0).gameObject.SetActive(false);
        _mainCanvas.enabled = false;

        Instantiate(_reactorExplosionParticle, transform.position, transform.rotation);
        LeanTween.alpha(_fadeImage.rectTransform, 1f, 2f).setOnComplete(SwitchScene);
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
