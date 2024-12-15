using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private EnemyStatus status;
    [SerializeField] private EnemyAnimation anim;
    [SerializeField] private NavMeshAgent agent;

    [Header("Status")]
    [SerializeField] private float checkCoolTime = 0.3f;
    [SerializeField] private float stopRange = 2f;
    private WaitForSeconds wfCheckCoolTime;

    private void Awake()
    {
        wfCheckCoolTime = new WaitForSeconds(checkCoolTime);

        status.OnReset.RemoveListener(ResetMove);
        status.OnReset.AddListener(ResetMove);

        status.OnPlayerDetected.RemoveListener(OnPlayerDetected);
        status.OnPlayerDetected.AddListener(OnPlayerDetected);

        status.OnPlayerExit.RemoveListener(OnPlayerExit);
        status.OnPlayerExit.AddListener(OnPlayerExit);
    }

    private void ResetMove()
    {
        if (status.IsPlayerDetected == false)
            return;

        StartCoroutine(CoMoveStop());
    }

    private IEnumerator CoMoveStop()
    {
        while(true)
        {
            Vector3 player = status.PlayerTransform.position;
            player.y = transform.position.y;
            transform.LookAt(player);

            float dist = (status.PlayerTransform.position - transform.position).magnitude;
            
            if (dist < stopRange)
            {
                agent.isStopped = true;
                anim.IsMoving(false);
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(status.PlayerTransform.position);
                anim.IsMoving(true);
            }

            yield return wfCheckCoolTime;
        }
    }

    private void OnPlayerDetected(Transform _player)
    {
        StartCoroutine(CoMoveStop());
    }

    private void OnPlayerExit()
    {
        StopAllCoroutines();
    }
}
