using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MonsterAI : MonoBehaviour
{
    public enum MonsterType
    {
        Doll,
        BookheadMonster
    }

    [Header("���� Ÿ�� ����")]
    public MonsterType monsterType = MonsterType.BookheadMonster;

    [Header("�� Ÿ�Ժ� ����/���� ��� ����")]
    public bool dollCanChaseAndAttack = false;
    public bool bookheadCanChaseAndAttack = true;

    [Header("���� ����")]
    public Transform player;
    public float chaseDistance = 8f;       // ���� ���� �Ÿ�
    public float attackDistance = 2f;      // ���� ����
    public float wanderRadius = 10f;       // ���� �ݰ�
    public float wanderTimer = 5f;         // ���� ����
    public float attackDuration = 1.2f;    // ���� �ִϸ��̼� ���� �ð�

    private NavMeshAgent agent;
    private Animator animator;
    private float timer;
    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        timer = wanderTimer;
    }

    void Update()
    {
        // 1) ���� �� ���Ͱ� ������/���� ��� ���¡����� �Ǻ�
        bool isEnabled = (monsterType == MonsterType.Doll)
                            ? dollCanChaseAndAttack
                            : bookheadCanChaseAndAttack;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 2) ��� �����̰�, �Ÿ� ������ ������ ���� ����/����
        if (isEnabled && distanceToPlayer <= attackDistance && !isAttacking)
        {
            // ���� ����
            agent.SetDestination(transform.position);
            animator.speed = 1f;
            SetAnimation(false, true);
            StartCoroutine(EndAttackAfter(attackDuration));
        }
        else if (isEnabled && distanceToPlayer <= chaseDistance && !isAttacking)
        {
            // ���� ����
            agent.SetDestination(player.position);
            animator.speed = 3f;
            SetAnimation(true, false);
        }
        else
        {
            // ���� ����
            if (!isAttacking)
            {
                timer += Time.deltaTime;
                if (timer >= wanderTimer)
                {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    agent.SetDestination(newPos);
                    timer = 0f;
                }

                bool isMoving = agent.velocity.magnitude > 0.1f;
                animator.speed = 1f;
                SetAnimation(isMoving, false);
            }
        }
    }

    void SetAnimation(bool isWalking, bool isAttacking)
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isAttacking", isAttacking);
    }

    IEnumerator EndAttackAfter(float seconds)
    {
        isAttacking = true;
        yield return new WaitForSeconds(seconds);
        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    // ============================
    // 3) public �޼��� �߰�: �� bool�� �Ѱ� �� �� �ֵ���
    // ============================
    /// <summary>
    /// �� ���Ͱ� ���� Ÿ�Կ� ���߾� chase/attack ���(true) �Ǵ� �����(false)���� �����Ѵ�.
    /// </summary>
    public void SetChaseAndAttackEnabled(bool enabled)
    {
        if (monsterType == MonsterType.Doll)
            dollCanChaseAndAttack = enabled;
        else if (monsterType == MonsterType.BookheadMonster)
            bookheadCanChaseAndAttack = enabled;
    }

    /// <summary>
    /// ���� ������ ����/������ Ȱ��ȭ�Ѵ�.
    /// </summary>
    public void EnableChaseAndAttack()
    {
        SetChaseAndAttackEnabled(true);
    }

    /// <summary>
    /// ���� ������ ����/������ ��Ȱ��ȭ�Ѵ�.
    /// </summary>
    public void DisableChaseAndAttack()
    {
        SetChaseAndAttackEnabled(false);
    }

    public bool IsChasingPlayer()
{
    float distanceToPlayer = Vector3.Distance(transform.position, player.position);

    bool isEnabled = (monsterType == MonsterType.Doll)
                        ? dollCanChaseAndAttack
                        : bookheadCanChaseAndAttack;

    // 추적 중이면 true
    return isEnabled && distanceToPlayer <= chaseDistance && distanceToPlayer > attackDistance;
}
}
