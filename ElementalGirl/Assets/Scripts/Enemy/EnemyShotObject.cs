using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotObject : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody rb;

    [Header("SkillStatus")]
    [SerializeField] private int damage = 5;
    [SerializeField] private float shotSpeed = 1f;
    [SerializeField] private float lifeTime = 3f;
    private WaitForSeconds wfLifeTime = null;

    [Header("Collide")]
    [SerializeField] private string playerTag = "Player";

    private EnemyShot enemy;
    private EnemyStatus status;

    public void Init(EnemyShot _enemy, EnemyStatus _status)
    {
        enemy = _enemy;
        status = _status;
        wfLifeTime = new WaitForSeconds(lifeTime);
    }

    public void Shot(Transform _parent, Transform _player)
    {
        transform.rotation = _parent.rotation;
        transform.position = _parent.position;

        Vector3 dir = status.IsPlayerDetected ? (_player.position + Vector3.up * 1.5f - transform.position) : transform.forward;
        rb.velocity = dir * shotSpeed;
        rb.AddTorque(transform.right * 300f);
        rb.maxAngularVelocity = 300f;
        StartCoroutine(CoReset());
    }

    private IEnumerator CoReset()
    {
        yield return wfLifeTime;
        ReturnToQueue();
    }

    private void ReturnToQueue()
    {
        StopAllCoroutines();

        rb.velocity = Vector3.zero;
        enemy.EnqueueShotObj(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            CollideWithPlayer(other.gameObject);
        }
    }

    private void CollideWithPlayer(GameObject other)
    {
        PlayerHP hp = other.GetComponentInParent<PlayerHP>();

        if (hp == null)
            return;

        hp.GetDamaged(damage);

        ReturnToQueue();
    }
}
