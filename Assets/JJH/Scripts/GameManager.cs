using UnityEngine;

public enum GamePhase
{
    Title,
    Ingame,
    GameOver
}

public class GameManager : MonoBehaviour
{

    [Header("몬스터 사망 여부")]
    public bool isDemonDollDead = false;
    public bool isBookHeadDead = false;
    public bool isZombieDead = false;

    public static GameManager Instance { get; private set; }

    [Header("게임 상태")]
    public GamePhase CurrentPhase { get; private set; } = GamePhase.Title;



    private void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPhase(GamePhase phase)
    {
        CurrentPhase = phase;
        Debug.Log($"🔄 게임 상태 변경: {phase}");
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    // ✅ 몬스터 사망 상태 설정 함수들
    public void SetDemonDollDead(bool dead) => isDemonDollDead = dead;
    public void SetBookHeadDead(bool dead) => isBookHeadDead = dead;
    public void SetZombieDead(bool dead) => isZombieDead = dead;

    // ✅ 몬스터 생존 여부 조회 함수들
    public bool IsDemonDollDead() => isDemonDollDead;
    public bool IsBookHeadDead() => isBookHeadDead;
    public bool IsZombieDead() => isZombieDead;
}
