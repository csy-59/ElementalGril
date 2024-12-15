using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private EnemyStatus status;
    [SerializeField] private EnemyAnimation anim;
    [SerializeField] private AnimationClip deathClip;
    [SerializeField] private int maxHP = 10;
    private int currentHP = 0;

    private EnemyManager manager;
    private WaitForSeconds wfDeath;

    public UnityEvent OnDeath { get; private set; }

    private void Awake()
    {
        currentHP = maxHP;
        status.IsAlive = true;

        wfDeath = new WaitForSeconds(deathClip.length);

        status.OnInit.RemoveListener(SetManager);
        status.OnInit.AddListener(SetManager);
    }

    private void SetManager(EnemyManager _manager)
    {
        manager = _manager;
    }

    public void GetDamage(int _damage)
    {
        currentHP -= maxHP;
        if(currentHP <= 0)
        {
            Death();
        }

        Hit();
    }

    private void Death()
    {
        anim.OnDeath();
        status.IsAlive = false;
        OnDeath?.Invoke();
        StartCoroutine(CoDeath());
    }

    private IEnumerator CoDeath()
    {
        yield return wfDeath;
        manager.EnqueueEnemey(status);
    }

    private void Hit()
    {
        anim.OnHit();
    }
}
