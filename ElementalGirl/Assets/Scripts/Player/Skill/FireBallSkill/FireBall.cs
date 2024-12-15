using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody rb;

    [Header("SkillStatus")]
    [SerializeField] private float shotSpeed = 2f;
    [SerializeField] private float lifeTime = 5f;
    private WaitForSeconds wfLifeTime = null;

    [Header("Collide")]
    [SerializeField] private string vineTag = "Vine";
    [SerializeField] private string enemyTag = "Enemy";

    private FireBallSkill skill;

    public void Init(FireBallSkill _skill, Transform _parent)
    {
        skill = _skill;
        wfLifeTime = new WaitForSeconds(lifeTime);
    }

    public void Shot(Transform _parent)
    {
        transform.rotation = _parent.rotation;
        transform.position = _parent.position;

        rb.velocity = transform.forward * shotSpeed;
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
        skill.EnqueueFireBall(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(vineTag))
        {
            CollideWithVine();
        }
        else if(collision.gameObject.CompareTag(enemyTag))
        {
            CollideWithEnemy();
        }
    }

    private void CollideWithVine()
    {

        ReturnToQueue();
    }

    private void CollideWithEnemy()
    {

        ReturnToQueue();
    }
}