using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    public AudioClip attackSound;     // 🔊 재생할 소리
    public float soundDelay = 0.5f;   // ⏱️ 몇 초 뒤에 소리 낼지 설정
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("✅ 마우스 클릭 - 공격 트리거 발생");
            animator.SetTrigger("attackTrigger");

            // ⏱️ 소리 재생 예약
            StartCoroutine(PlaySoundAfterDelay(soundDelay));
        }
    }

    IEnumerator PlaySoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
        else
        {
            Debug.LogWarning("⚠️ attackSound가 설정되지 않았습니다.");
        }
    }
}
