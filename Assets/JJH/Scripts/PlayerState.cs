using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    [Header("상태 저장")]
    private int letterCount = 0;
    private bool hasCrowbar = false;
    private bool hasKey = false;

    [Header("Escape UI")]
    public GameObject escapeUI;

    [Header("Enemy 참조")]
    public GameObject enemyA;
    public GameObject enemyB;

    void Update()
    {
        CheckHiddenEndingTrigger();
        CheckDeadEndingTrigger();
    }

    // 📩 Letter 획득
    public void CollectLetter()
    {
        letterCount++;
        Debug.Log($"📩 편지 {letterCount}개 획득");
    }

    public int GetLetterCount() => letterCount;

    public bool HiddenEndingCase() => letterCount >= 5;

    // 🔧 Crowbar
    public void ObtainCrowbar()
    {
        hasCrowbar = true;
        Debug.Log("🔧 Crowbar 획득");
    }

    // 🗝️ Key
    public void ObtainKey()
    {
        hasKey = true;
        Debug.Log("🗝️ 열쇠 획득");
    }

    public bool HasKey() => hasKey;

    // 🎯 탈출 시도 (열쇠 있을 때 InteractionManager에서 호출)
    public void CheckEscapeTrigger()
    {
        GameManager.Instance.SetPhase(GamePhase.GameOver);
        SceneManager.LoadScene("EscapeScene");
    }

    // ✅ 히든엔딩 조건 확인: Update에서 체크
    private void CheckHiddenEndingTrigger()
    {
        if (HiddenEndingCase() && enemyA == null && enemyB == null)
        {
            Debug.Log("🎉 히든 엔딩 조건 만족");
            GameManager.Instance.SetPhase(GamePhase.GameOver);
            SceneManager.LoadScene("HiddenEndingScene");
        }
    }

    // ☠️ 적에게 잡힘
    private void CheckDeadEndingTrigger()
    {
        CheckOneEnemy(enemyA);
        CheckOneEnemy(enemyB);
    }

private void CheckOneEnemy(GameObject enemy)
{
    if (enemy == null) return;

    var ai = enemy.GetComponent<MonsterAI>();
    if (ai != null && ai.IsChasingPlayer()) // ✅ 상태 체크 메서드로 교체
    {
        float dist = Vector3.Distance(transform.position, ai.transform.position);
        if (dist <= 5f)
        {
            Debug.Log("☠️ 적에게 잡힘 - DeadEnding");
            GameManager.Instance.SetPhase(GamePhase.GameOver);
            GameSceneManager.Instance.LoadDeadEndingScene();
        }
    }
}
}
