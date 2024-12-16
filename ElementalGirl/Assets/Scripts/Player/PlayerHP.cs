using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int maxHp = 100;
    public int MaxHp { get => maxHp; }
    private int currentHp;

    public UnityEvent OnPlayerDeath { get; private set; } = new UnityEvent();
    public UnityEvent<int> OnPlayerHit { get; private set; } = new UnityEvent<int>();
    public UnityEvent OnPlayerFall { get; private set; } = new UnityEvent();

    private void Awake()
    {
        currentHp = maxHp;
    }
    public void PlayerFall()
    {
        GetDamaged(35);
        if(currentHp > 0)
            OnPlayerFall?.Invoke();
    }

    public void FullHealth()
    {
        currentHp = maxHp;
        OnPlayerHit?.Invoke(currentHp);
    }

    public void GetDamaged(int _damage)
    {
        currentHp -= _damage;
        OnPlayerHit?.Invoke(currentHp);
        if(currentHp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        OnPlayerDeath?.Invoke();
    }

}
