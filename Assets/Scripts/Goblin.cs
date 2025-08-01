using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static MonsterCore;

public class Goblin : MonsterCore
{
    private float timer;
    private float idleTime, patrolTime;

    private float traceDist = 5f;

    private float attackDist = 1.5f;
    private bool isAttack;



    void Start()
    {
        Init(10f, 3f, 2f, 1f);
    }

    protected override void Init(float hp, float speed, float attackTime, float atkDamage)
    {
        base.Init(hp, speed, attackTime, atkDamage);
    }

    public override void Idle()
    {
        timer += Time.deltaTime;
        if (timer >= idleTime)
        {
            timer = 0f;
            moveDir = Random.Range(0, 2) == 1 ? 1 : -1;
            transform.localScale = new Vector3(moveDir, 1, 1);
            patrolTime = Random.Range(1f, 5f);
            animator.SetBool("isRun", true);

            ChangeState(MonsterState.PATROL);
        }

        if (targetDist <= traceDist && isTrace)
        {
            timer = 0f;
            animator.SetBool("isRun", true);
            ChangeState(MonsterState.TRACE);
        }
    }

    public override void Patrol()
    {
        transform.position += Vector3.right * moveDir * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= patrolTime)
        {
            timer = 0f;
            idleTime = Random.Range(1f, 5f);
            animator.SetBool("isRun", false);

            ChangeState(MonsterState.IDLE);
        }

        if (targetDist <= traceDist && isTrace)
        {
            timer = 0f;
            ChangeState(MonsterState.TRACE);
        }
    }

    public override void Trace()
    {
        var targetDir = (target.position - transform.position).normalized;
        transform.position += Vector3.right * targetDir.x * speed * Time.deltaTime;

        var scaleX = targetDir.x > 0 ? 1 : -1;
        transform.localScale = new Vector3(scaleX, 1, 1);

        if (targetDist > traceDist)
        {
            animator.SetBool("isRun", false);
            ChangeState(MonsterState.IDLE);
        }

        if (targetDist < attackDist)
        {
            ChangeState(MonsterState.ATTACK);
        }
    }

    public override void Attack()
    {
        if (!isAttack)
            StartCoroutine(AttackRoutine());
    }

    public override void Skill()
    {

    }
    IEnumerator AttackRoutine()
    {
        isAttack = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        animator.SetBool("isRun", false);

        yield return new WaitForSeconds(attackTime - 1f);
        isAttack = false;
        ChangeState(MonsterState.IDLE);
    }
   

}