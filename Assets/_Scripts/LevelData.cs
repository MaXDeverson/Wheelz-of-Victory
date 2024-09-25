using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public Action<Transform> OnEnemySpawnAction { get; set; }
    public int LevelTime { get => _levelTime; }
    [SerializeField] private int _levelTime;
    [SerializeField] private Transform _enemySpawnPoint;
    [SerializeField] private List<float> _enemyWaveTime;
    [SerializeField] private List<Transform> _path;
    [SerializeField] private List<GameObject> _enemyWaveQueue;
    private int _currentEnemyIndex = 0;
    private List<EnemyMoveLogic> _enemyMovers = new List<EnemyMoveLogic>();
    private bool _spawnEnemies = true;
    public void StopAllEnemies()
    {
        StopAllCoroutines();
        for(int i = 0;i< _enemyMovers.Count;i++)
        {
            if (_enemyMovers[i] != null)
            {
                _spawnEnemies = false;
                _enemyMovers[i].Move(false);
            }
        }
    }
    void Start()
    {
        if(_enemyWaveQueue.Count != _enemyWaveTime.Count)
        {
            Debug.LogError("Enemy and time not equals in count");
            return;
        }
        StartCoroutine(SpawnEnemy());
    }
    
    private IEnumerator SpawnEnemy()
    {
        if (_spawnEnemies)
        {
            float time = _enemyWaveTime[_currentEnemyIndex];
            GameObject enemy = _enemyWaveQueue[_currentEnemyIndex];
            _currentEnemyIndex++;
            if (_currentEnemyIndex >= _enemyWaveQueue.Count)
            {
                _currentEnemyIndex = 0;
            }
            yield return new WaitForSeconds(time);
            GameObject spawnedEnemy = Instantiate(enemy, _enemySpawnPoint.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemyMoveLogic>().SetPath(_path);
            _enemyMovers.Add(spawnedEnemy.GetComponent<EnemyMoveLogic>());
            StartCoroutine(SpawnEnemy());
            yield return new WaitForSeconds(2);
            OnEnemySpawnAction?.Invoke(spawnedEnemy.transform);
        }
       
    }

}
