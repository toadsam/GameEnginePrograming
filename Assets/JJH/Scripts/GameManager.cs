using UnityEngine;

public enum GamePhase
{
    Title,
    Ingame,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("게임 상태")]
    public GamePhase CurrentPhase { get; private set; } = GamePhase.Title;

    [Header("편지 UI 배열")]
    public GameObject[] letterDetails;

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
}
