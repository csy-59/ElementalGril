using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody rb;

    [Header("SkillStatus")]
    [SerializeField] private int damage = 4;
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
        skill.EnqueueFireBall(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(vineTag))
        {
            CollideWithVine(other.gameObject);
        }
        else if(other.gameObject.CompareTag(enemyTag))
        {
            CollideWithEnemy(other.gameObject);
        }
    }

    private void CollideWithVine(GameObject other)
    {
        Vine vine = other.GetComponentInParent<Vine>();
        
        if (vine == null)
            return;
        
        vine.SetFire();

        ReturnToQueue();
    }

    private void CollideWithEnemy(GameObject other)
    {
        EnemyHP hp = other.GetComponentInParent<EnemyHP>();

        if (hp == null)
            return;

        hp.GetDamage(damage);

        ReturnToQueue();
    }
}
