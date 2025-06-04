using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("✅ 마우스 클릭 - 공격 트리거 발생");
            animator.SetTrigger("attackTrigger"); // Trigger 발동
        }
    }
}
