using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class MonsterCore : MonoBehaviour, IDamageable
{
    public enum MonsterState { IDLE, PATROL, TRACE, ATTACK, SKILL }
    public MonsterState monsterState = MonsterState.IDLE;


    public ItemManager itemManager;

    protected Animator animator;
    protected Rigidbody2D monsterRb;
    protected Collider2D monsterColl;

    public Transform target;

    public float maxHp;
    public float speed;
    public float attackTime;

    protected float moveDir;

    protected float targetDist;

    protected bool isTrace;

    protected bool isDead;

    public Image hpBar;
    private float currHp;

    public float atkDamage;

    protected virtual void Init(float hp, float speed, float attackTime, float atkDamage)
    {
        this.maxHp = hp;
        this.speed = speed;
        this.attackTime = attackTime;
        this.atkDamage = atkDamage;

        itemManager = FindFirstObjectByType<ItemManager>();

        target = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();
        monsterRb = GetComponent<Rigidbody2D>();
        monsterColl = GetComponent<Collider2D>();


        currHp = hp;
        hpBar.fillAmount = currHp / hp;
    }

    void Update()
    {
        if (isDead)
            return;

        targetDist = Vector3.Distance(transform.position, target.position);

        Vector3 monsterDir = Vector3.right * moveDir;
        Vector3 playerDir = (transform.position - target.position).normalized;

        float dotValue = Vector3.Dot(monsterDir, playerDir);
        isTrace = dotValue < -0.5f && dotValue >= -1f;

        switch (monsterState)
        {
            case MonsterState.IDLE:
                Idle();
                break;
            case MonsterState.PATROL:
                Patrol();
                break;
            case MonsterState.TRACE:
                Trace();
                break;
            case MonsterState.ATTACK:
                Attack();
                break;
            case MonsterState.SKILL:
                Skill();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Return"))
        {
            moveDir *= -1;
            transform.localScale = new Vector3(moveDir, 1, 1);
        }

        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDamage(atkDamage);
        }
    }

    public abstract void Idle();
    public abstract void Patrol();
    public abstract void Trace();
    public abstract void Attack();
    public abstract void Skill();

    public void ChangeState(MonsterState newState)
    {
        if (monsterState != newState)
            monsterState = newState;
    }
    public void TakeDamage(float damage)
    {
        currHp -= damage;
        hpBar.transform.localScale = new Vector3(currHp / maxHp, 1, 1);
        //hpBar.fillAmount = currHp / hp;
        animator.SetTrigger("Hit");
        if (currHp <= 0f)
            Death();
    }
    public void Death()
    {
        isDead = true;
        monsterColl.enabled = false;
        animator.SetTrigger("Death");
        monsterRb.gravityScale = 0f;

        int itemCount = UnityEngine.Random.Range(0, 3); // 0, 1, 2
        if (itemCount > 0) // 혹시나 0이 나오면 에러가 발생하기 때문에 예외처리
        {
            for (int i = 0; i < itemCount; i++) // itemCount 값으로 반복문 실행
            {
                itemManager.DropItem(transform.position); // 드롭 아이템 생성
            }
        }
    }
}
