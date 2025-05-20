using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 8f;       // �÷��̾ �ѱ� �����ϴ� �Ÿ�
    public float attackDistance = 2f;      // ���� ����
    public float wanderRadius = 10f;       // ���� �ݰ�
    public float wanderTimer = 5f;         // ���� ����
    public float attackDuration = 1.2f;    // ���� �ִϸ��̼� �ð� (�ʿ� �� Ŭ�� ���̿� �°� ����)

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
            // ���� ����
            agent.SetDestination(transform.position); // ����
            animator.speed = 1.0f;
            SetAnimation(false, true);
            StartCoroutine(EndAttackAfter(attackDuration));
        }
        else if (distanceToPlayer <= chaseDistance && !isAttacking)
        {
            // �÷��̾� ����
            agent.SetDestination(player.position);
            animator.speed = 3.0f; // ������ �ȴ� ��ó��
            SetAnimation(true, false);
        }
        else if (!isAttacking)
        {
            // ����
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
