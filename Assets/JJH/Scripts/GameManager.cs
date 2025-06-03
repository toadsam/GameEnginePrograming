using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }

    [Header("씬에 배치된 몬스터 레퍼런스")]
    public MonsterAI dollMonster;
    public MonsterAI bookheadMonster;

    void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // 예시: 게임 시작하자마자 둘 다 OFF
        if (dollMonster != null)
            dollMonster.DisableChaseAndAttack();

        if (bookheadMonster != null)
            bookheadMonster.DisableChaseAndAttack();
    }

    // UI 버튼이나 이벤트에서 호출할 수 있도록 공개 메서드
    public void ToggleDollBehavior(bool on)
    {
        if (dollMonster != null)
            dollMonster.SetChaseAndAttackEnabled(on);
    }

    public void ToggleBookheadBehavior(bool on)
    {
        if (bookheadMonster != null)
            bookheadMonster.SetChaseAndAttackEnabled(on);
    }
}
