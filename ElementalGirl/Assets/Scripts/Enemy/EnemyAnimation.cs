using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private static int OnDeathID = Animator.StringToHash("OnDeath");
    private static int OnHitID = Animator.StringToHash("OnHit");
    private static int IsDizzyID = Animator.StringToHash("IsDizzy");
    private static int IsMovingID = Animator.StringToHash("IsMoving");

    [SerializeField] private Animator anim;

    public void OnDeath()
    {
        anim.SetTrigger(OnDeathID);
    }

    public void OnHit()
    {
        anim.SetTrigger(OnHitID);
    }

    public void IsDizzy(bool _isDizzy)
    {
        anim.SetBool(IsDizzyID, _isDizzy);
    }

    public void IsMoving(bool _isMoving)
    {
        anim.SetBool(IsMovingID, _isMoving);
    }
}
