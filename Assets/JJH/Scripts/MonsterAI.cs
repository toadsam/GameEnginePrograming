using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 8f;
    public float attackDistance = 2f;
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;

    private NavMeshAgent agent;
    private Animator animator;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        timer = wanderTimer;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance)
        {
            // °ø°Ý ¾Ö´Ï¸ÞÀÌ¼Ç
            agent.SetDestination(transform.position);  // ¸ØÃã
            SetAnimation(false, false, true);
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            // ÇÃ·¹ÀÌ¾î ÂÑ±â
            agent.SetDestination(player.position);
            SetAnimation(false, true, false);
        }
        else
        {
            // ·£´ý ¼øÂû
            timer += Time.deltaTime;
            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }

            bool isMoving = agent.velocity.magnitude > 0.1f;
            SetAnimation(isMoving, false, false);
        }
    }

    void SetAnimation(bool isWalking, bool isRunning, bool isAttacking)
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isAttacking", isAttacking);
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
