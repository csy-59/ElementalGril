using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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

    private UnityEvent<Transform> OnPlayerDetacted = new UnityEvent<Transform> ();
    private UnityEvent OnPlayerExit = new UnityEvent();

    private void Start()
    {
        InitializeEnemies();
    }

    private void InitializeEnemies()
    {
        enemyQueue = new Queue<EnemyStatus> ();

        for(int i = 0; i < maxEnemyCount; ++i)
        {
            GameObject e = Instantiate(enemyPrefab);

            EnemyStatus status = e.GetComponent<EnemyStatus>();
            status.Init(this);
            OnPlayerDetacted.AddListener(status.PlayerDetected);
            OnPlayerExit.AddListener(status.PlayerExit);
            
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

        nextEnemy.gameObject.SetActive(true);
        nextEnemy.ResetEnemy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;

        Transform player = other.transform;
        while(true)
        {
            if (player == null) break;

            if (player.TryGetComponent<PlayerHP>(out var _) == true)
                break;

            player = player.parent;
        }

        OnPlayerDetacted?.Invoke(player);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;

        OnPlayerExit?.Invoke();
    }
}
