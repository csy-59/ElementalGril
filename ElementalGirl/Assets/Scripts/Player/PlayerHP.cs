using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int maxHp = 100;
    private int currentHp;

    public UnityEvent OnPlayerDeath { get; private set; }
    public UnityEvent<int> OnPlayerHit { get; private set; }

    private void Awake()
    {
        currentHp = maxHp;
    }

    public void GetDamaged(int _damage)
    {
        currentHp -= _damage;
        if(currentHp <= 0)
        {
            Death();
        }
        else
        {
            OnPlayerHit?.Invoke(currentHp);
        }
    }

    private void Death()
    {
        OnPlayerDeath?.Invoke();
    }
}
