using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems; // UI 클릭 감지용

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    public AudioClip attackSound;
    public float soundDelay = 0.5f;
    private AudioSource audioSource;

    [Header("숨겼다 다시 보여줄 UI")]
    public GameObject uiToToggle;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // 👉 ESC 키 누르면 UI 다시 보이기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (uiToToggle != null)
            {
                uiToToggle.SetActive(true);
                Debug.Log("🔓 ESC 누름 - UI 다시 표시");
            }
        }

        // 👉 마우스 클릭 (UI 클릭 중이 아닐 때만)
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("✅ 마우스 클릭 - 공격 트리거 발생");

            // 애니메이션
            animator.SetTrigger("attackTrigger");

            // 사운드 재생 예약
            StartCoroutine(PlaySoundAfterDelay(soundDelay));

            // UI 숨기기
            if (uiToToggle != null && uiToToggle.activeSelf)
            {
                uiToToggle.SetActive(false);
                Debug.Log("👋 UI 숨김");
            }
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
