using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStatus : MonoBehaviour
{
    public bool IsAlive { get; set; } = true;

    public bool IsDizzy { get; set; } = false;

    public bool IsMoving { get; set; } = false;


    public bool IsPlayerDetected { get; private set; } = false;
    public Transform PlayerTransform { get; private set; } = null;


    public UnityEvent<EnemyManager> OnInit { get; private set; } = new UnityEvent<EnemyManager>();
    public UnityEvent OnReset { get; private set; } = new UnityEvent();
    public UnityEvent<Transform> OnPlayerDetected { get; private set; } = new UnityEvent<Transform>();
    public UnityEvent OnPlayerExit { get; private set; } = new UnityEvent(); 

    public void Init(EnemyManager _manager)
    {
        OnInit?.Invoke(_manager);
    }

    public void ResetEnemy()
    {
        OnReset?.Invoke();
    }

    public void PlayerDetected(Transform _player)
    {
        IsPlayerDetected = true;
        PlayerTransform = _player;
        OnPlayerDetected?.Invoke(PlayerTransform);
    }

    public void PlayerExit()
    {
        IsPlayerDetected = false;
        PlayerTransform = null;
        OnPlayerExit?.Invoke();
    }
}
