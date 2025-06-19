using System.Collections;
using UnityEngine;
using static MonsterAI;

public class DisappearOnWeaponHit : MonoBehaviour
{
    [Header("몬스터 유형 지정")]
    public MonsterType monsterType;

    [Header("맞으면 3초 후 사라질 오브젝트")]
    public GameObject targetObject;

    [Header("맞는 태그")]
    public string weaponTag = "Weapon";

    [Header("충돌 시 재생할 사운드")]
    public AudioClip hitSound;

    [Header("외부 Animator (예: Doll Animator)")]
    public Animator targetAnimator;

    private AudioSource audioSource;
    private bool isDead = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        // ✅ MonsterAI에서 monsterType 가져오기
        if (TryGetComponent(out MonsterAI ai))
        {
            monsterType = ai.monsterType;
            Debug.Log($"🧠 자동 설정됨: {gameObject.name} → {monsterType}");
        }
    }

    private void Update()
    {
        // ✅ 테스트용: T 키 누르면 전부 사망 처리
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("🧪 테스트: T 키 눌림 - 전 몬스터 사망 처리");

            GameManager.Instance.SetDemonDollDead(true);
            GameManager.Instance.SetBookHeadDead(true);
            GameManager.Instance.SetZombieDead(true);
            StartCoroutine(RemoveAfterDelay(2f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag(weaponTag))
        {
            isDead = true;

            if (hitSound != null)
                audioSource.PlayOneShot(hitSound);

            if (targetAnimator != null)
                targetAnimator.SetBool("isDie", true);

            // ✅ 실제 사망 처리 + 디버그 로그 출력
            switch (monsterType)
            {
                case MonsterType.Doll:
                    GameManager.Instance.SetDemonDollDead(true);
                    Debug.Log("💀 Demon Doll이 죽었습니다.");
                    break;
                case MonsterType.BookheadMonster:
                    GameManager.Instance.SetBookHeadDead(true);
                    Debug.Log("📕 Bookhead Monster가 죽었습니다.");
                    break;
                case MonsterType.Zombie:
                    GameManager.Instance.SetZombieDead(true);
                    Debug.Log("🧟 Zombie가 죽었습니다.");
                    break;
            }

            StartCoroutine(RemoveAfterDelay(2f));
        }
    }


    private IEnumerator RemoveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (targetObject != null)
            Destroy(targetObject);
    }
}
