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


    public UnityEvent<EnemyManager> OnInit { get; private set; }
    public UnityEvent OnReset { get; private set; }

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
    }
}
