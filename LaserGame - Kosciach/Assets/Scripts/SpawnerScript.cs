using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [Header("----References------------")]
    [Tooltip("Point used to determine where enemy should spawn")]
    [SerializeField] List<Vector3> _spawnPoints;
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _enemyPrefab;

    [Header("----Timer------------")]
    [Tooltip("Clock that goes towards 0")]
    [SerializeField] float _timeToSpawn;

    [Tooltip("Value that clock is set to when reaches 0")]
    [SerializeField] float _timeBetweenSpawns;

    [Tooltip("Determines how fast time will pass")]
    [SerializeField] float _timeSpeed;

    [Tooltip("Determines how much time between spawns will be lowered")]
    [SerializeField] float _timeLowerer;

    [Tooltip("Determines minimal time between spawns")]
    [SerializeField] float _minSpawnTime;

    private void Awake()
    {
        _spawnPoints.Add(new Vector3(0f, _camera.orthographicSize + 1, 0f));
        _spawnPoints.Add(new Vector3(0f, -_camera.orthographicSize - 1, 0f));
    }
    private void Start()
    {
        _timeToSpawn = _timeBetweenSpawns;
    }

    private void Update()
    {
        _timeToSpawn -= _timeSpeed * Time.deltaTime;
        if(_timeToSpawn <= 0)
        {
            GameObject spawnedEnemy = Instantiate(_enemyPrefab, DetermineSpawnPoint(), Quaternion.identity);

            if (_timeBetweenSpawns > _minSpawnTime) _timeBetweenSpawns -= _timeLowerer;
            _timeToSpawn = _timeBetweenSpawns;
        }
    }


    private Vector3 DetermineSpawnPoint()
    {
        int poleChoice = Random.Range(0,2);
        float halfCameraWidth = _camera.orthographicSize * _camera.aspect;
        Vector3 spawnOffset = new Vector3(Random.Range(-halfCameraWidth, halfCameraWidth), 0f, 0f);
        return _spawnPoints[poleChoice] + spawnOffset;
    }
}
