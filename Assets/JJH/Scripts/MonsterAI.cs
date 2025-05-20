using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 8f;       // 플레이어를 쫓기 시작하는 거리
    public float attackDistance = 2f;      // 공격 범위
    public float wanderRadius = 10f;       // 순찰 반경
    public float wanderTimer = 5f;         // 순찰 간격
    public float attackDuration = 1.2f;    // 공격 애니메이션 시간 (필요 시 클립 길이에 맞게 수정)

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
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance && !isAttacking)
        {
            // 공격 상태
            agent.SetDestination(transform.position); // 멈춤
            animator.speed = 1.0f;
            SetAnimation(false, true);
            StartCoroutine(EndAttackAfter(attackDuration));
        }
        else if (distanceToPlayer <= chaseDistance && !isAttacking)
        {
            // 플레이어 추적
            agent.SetDestination(player.position);
            animator.speed = 3.0f; // 빠르게 걷는 것처럼
            SetAnimation(true, false);
        }
        else if (!isAttacking)
        {
            // 순찰
            timer += Time.deltaTime;
            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }

            bool isMoving = agent.velocity.magnitude > 0.1f;
            animator.speed = 1.0f;
            SetAnimation(isMoving, false);
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
}
