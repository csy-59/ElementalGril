using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private EnemyStatus status;

    [Header("Queue")]
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private int initFireBallCount = 5;
    private Queue<EnemyShotObject> shotObjList;

    [Header("Shot")]
    [SerializeField] private Transform parentTransform;
    [SerializeField] private float shotRange = 3f;
    [SerializeField] private float minShotOffsetTime = 0.7f;
    [SerializeField] private float maxShotOffsetTime = 1.0f;
    [SerializeField] private float shotQualityCheckTime = 0.3f;
    private WaitForSeconds wfShotQualityCheck;
    private float nextShotOffsetTime = 0f;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    private void Awake()
    {
        InitializeShotObjList();

        wfShotQualityCheck = new WaitForSeconds(shotQualityCheckTime); 

        status.OnReset.RemoveListener(ResetShot);
        status.OnReset.AddListener(ResetShot);

        status.OnPlayerDetected.RemoveListener(OnPlayerDetected);
        status.OnPlayerDetected.AddListener(OnPlayerDetected);

        status.OnPlayerExit.RemoveListener(OnPlayerExit);
        status.OnPlayerExit.AddListener(OnPlayerExit);
    }


    private void ResetShot()
    {
        nextShotOffsetTime = 0f;
        if(status.IsPlayerDetected == true)
        {
            StartCoroutine(CoWaitForPlayerRange());
        }
    }


    private void InitializeShotObjList()
    {
        shotObjList = new Queue<EnemyShotObject>(initFireBallCount);

        for (int i = 0; i < initFireBallCount; i++)
        {
            InstantiateShotObj();
        }
    }

    private void InstantiateShotObj()
    {
        GameObject newFireBall = Instantiate(fireBallPrefab);
        EnemyShotObject script = newFireBall.GetComponent<EnemyShotObject>();

        script.Init(this, status);
        EnqueueShotObj(script);
    }

    public void EnqueueShotObj(EnemyShotObject _shotObj)
    {
        _shotObj.gameObject.SetActive(false);
        shotObjList.Enqueue(_shotObj);
    }

    private EnemyShotObject DequeueShotObj()
    {
        if (shotObjList.Count <= 0)
            InstantiateShotObj();

        return shotObjList.Dequeue();
    }


    private IEnumerator CoShot()
    {
        while(true)
        {
            yield return new WaitForSeconds(nextShotOffsetTime);

            if (CheckIfPlayerOnRange() == false)
                break;

            nextShotOffsetTime = GetShotOffsetTime();

            ShotObj();
        }
        StartCoroutine(CoWaitForPlayerRange());
    }

    private IEnumerator CoWaitForPlayerRange()
    {
        while(true)
        {
            if (CheckIfPlayerOnRange() == true)
                break;

            yield return wfShotQualityCheck;
        }
        StartCoroutine(CoShot());
    }

    private void OnPlayerDetected(Transform _player)
    {
        StopAllCoroutines();
        StartCoroutine(CoWaitForPlayerRange());
    }

    private void OnPlayerExit()
    {
        StopAllCoroutines();
    }

    private float GetShotOffsetTime()
    {
        return Random.Range(minShotOffsetTime, maxShotOffsetTime);
    }

    private bool CheckIfPlayerOnRange()
    {
        if (status.IsPlayerDetected == false)
            return false;

        float dist = (status.PlayerTransform.position - transform.position).magnitude;
        return dist <= shotRange;
    }

    private void ShotObj()
    {
        EnemyShotObject ball = DequeueShotObj();
        ball.gameObject.SetActive(true);
        ball.Shot(parentTransform, status.PlayerTransform);

        audioSource.PlayOneShot(clip);
    }
}
