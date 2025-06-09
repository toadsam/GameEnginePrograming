using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    public AudioClip attackSound;
    public float soundDelay = 0.5f;
    private AudioSource audioSource;

    [Header("I 키로 켜고 끌 UI")]
    public GameObject uiToToggle;

    void Start()
    {
        LockCursor();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // ✅ I 키로 UI 토글
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool isActive = uiToToggle.activeSelf;
            uiToToggle.SetActive(!isActive);

            if (isActive)
                LockCursor();   // UI 꺼짐 → 커서 잠금
            else
                UnlockCursor(); // UI 켜짐 → 커서 표시
        }

        // UI 켜져있으면 공격 안 함
        if (uiToToggle.activeSelf) return;

        // 좌클릭 시 공격
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attackTrigger");
            StartCoroutine(PlaySoundAfterDelay(soundDelay));
        }
    }

    IEnumerator PlaySoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (attackSound != null)
            audioSource.PlayOneShot(attackSound);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
