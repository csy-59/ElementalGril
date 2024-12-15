using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkill : SkillBase
{
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private int initFireBallCount = 5;
    private Queue<FireBall> fireBallList;
    private Transform parentTransform;

    public override void Init(Transform _parent)
    {
        parentTransform = _parent;
        InitializeFireBallList();
    }

    private void InitializeFireBallList()
    {
        fireBallList = new Queue<FireBall>(initFireBallCount);

        for(int i = 0; i< initFireBallCount; i++)
        {
            InstantiateFireBall();
        }
    }

    protected override void SkillLogic()
    {
        FireBall ball = DequeueFireBall();
        ball.gameObject.SetActive(true);
        ball.Shot(transform);
    }

    private void InstantiateFireBall()
    {
        GameObject newFireBall = Instantiate(fireBallPrefab);
        FireBall script = newFireBall.GetComponent<FireBall>();

        script.Init(this, parentTransform);
        EnqueueFireBall(script);
    }

    public void EnqueueFireBall(FireBall _fireBall)
    {
        _fireBall.gameObject.SetActive(false);
        fireBallList.Enqueue(_fireBall);
    }

    private FireBall DequeueFireBall()
    {
        if (fireBallList.Count <= 0)
            InstantiateFireBall();

        return fireBallList.Dequeue();
    }
}
