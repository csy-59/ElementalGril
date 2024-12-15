using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Initialize")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int maxEnemyCount;

    [Header("Resqawn")]
    [SerializeField] private Transform[] spawnPosition;
    [SerializeField] private float minEnemyRespawnTime = 0.3f;
    [SerializeField] private float maxEnemyRespawnTime = 0.6f;

    private Queue<EnemyStatus> enemyQueue;

    private void Start()
    {
        InitializeEnemies();
    }

    private void InitializeEnemies()
    {
        for(int i = 0; i < maxEnemyCount; ++i)
        {
            GameObject e = Instantiate(enemyPrefab);
            EnqueueEnemey(e.GetComponent<EnemyStatus>(), true);
        }
    }

    public void EnqueueEnemey(EnemyStatus _enemyStatus, bool _isForInit = false)
    {
        _enemyStatus.gameObject.SetActive(false);
        enemyQueue.Enqueue(_enemyStatus);

        if(_isForInit == false)
        {
            StartCoroutine(CoRespawnEnemy(minEnemyRespawnTime, maxEnemyRespawnTime));
        }
        else
        {
            StartCoroutine(CoRespawnEnemy(0f, 0f));
        }
    }

    private EnemyStatus DequeueEnemy()
    {
        if (enemyQueue.Count <= 0)
            return null;

        return enemyQueue.Dequeue();
    }

    private IEnumerator CoRespawnEnemy(float _minRespawnTime, float _maxRespawnTime)
    {
        float coolTime = Random.Range(_minRespawnTime, _maxRespawnTime);
        yield return new WaitForSeconds(coolTime);

        int index = Random.Range(0, spawnPosition.Length);
        Transform spawnTrns = spawnPosition[index];

        EnemyStatus nextEnemy = DequeueEnemy();

        if (nextEnemy == null)
            yield break;

        nextEnemy.transform.position = spawnTrns.position;
        nextEnemy.transform.rotation = spawnTrns.rotation;

        nextEnemy.ResetEnemy();
        nextEnemy.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;


    }
}
