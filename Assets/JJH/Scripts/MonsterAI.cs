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

    [Header("몬스터 타입 설정")]
    public MonsterType monsterType = MonsterType.BookheadMonster;

    [Header("각 타입별 추적/공격 허용 여부")]
    public bool dollCanChaseAndAttack = false;
    public bool bookheadCanChaseAndAttack = true;

    [Header("공통 설정")]
    public Transform player;
    public float chaseDistance = 8f;       // 추적 시작 거리
    public float attackDistance = 2f;      // 공격 범위
    public float wanderRadius = 10f;       // 순찰 반경
    public float wanderTimer = 5f;         // 순찰 간격
    public float attackDuration = 1.2f;    // 공격 애니메이션 지속 시간

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
        // 1) 현재 이 몬스터가 “추적/공격 허용 상태”인지 판별
        bool isEnabled = (monsterType == MonsterType.Doll)
                            ? dollCanChaseAndAttack
                            : bookheadCanChaseAndAttack;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 2) 허용 상태이고, 거리 조건이 만족될 때만 공격/추적
        if (isEnabled && distanceToPlayer <= attackDistance && !isAttacking)
        {
            // 공격 상태
            agent.SetDestination(transform.position);
            animator.speed = 1f;
            SetAnimation(false, true);
            StartCoroutine(EndAttackAfter(attackDuration));
        }
        else if (isEnabled && distanceToPlayer <= chaseDistance && !isAttacking)
        {
            // 추적 상태
            agent.SetDestination(player.position);
            animator.speed = 3f;
            SetAnimation(true, false);
        }
        else
        {
            // 순찰 상태
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
    // 3) public 메서드 추가: 각 bool을 켜고 끌 수 있도록
    // ============================
    /// <summary>
    /// 이 몬스터가 속한 타입에 맞추어 chase/attack 허용(true) 또는 비허용(false)으로 설정한다.
    /// </summary>
    public void SetChaseAndAttackEnabled(bool enabled)
    {
        if (monsterType == MonsterType.Doll)
            dollCanChaseAndAttack = enabled;
        else if (monsterType == MonsterType.BookheadMonster)
            bookheadCanChaseAndAttack = enabled;
    }

    /// <summary>
    /// 현재 몬스터의 추적/공격을 활성화한다.
    /// </summary>
    public void EnableChaseAndAttack()
    {
        SetChaseAndAttackEnabled(true);
    }

    /// <summary>
    /// 현재 몬스터의 추적/공격을 비활성화한다.
    /// </summary>
    public void DisableChaseAndAttack()
    {
        SetChaseAndAttackEnabled(false);
    }
}
