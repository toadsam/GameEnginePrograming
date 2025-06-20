using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public void LoadJJH3Scene()
    {
        SceneManager.LoadScene("JJH3");
    }
    public void LoadTitle()
    {
        ResetGameState(); ;
        SceneManager.LoadScene("StartScene");
    }

    private void ResetGameState()
    {
        if (GameManager.Instance != null)
        {
          //  GameManager.Instance.SetPhase(GamePhase.Title);
            GameManager.Instance.letterCount = 0;

            // 몬스터 상태 초기화
            GameManager.Instance.SetDemonDollDead(false);
            GameManager.Instance.SetBookHeadDead(false);
            GameManager.Instance.SetZombieDead(false);
            FullReset();
        }

        // 다른 매니저들 초기화 (예: 인벤토리, 점수 등)
        // InventoryManager.Instance?.Reset();
        // ScoreManager.Instance?.Reset();
    }

    public void FullReset()
    {
        Destroy(GameManager.Instance.gameObject);
        Destroy(GameSceneManager.Instance.gameObject); 
            Destroy(EnemyManager.Instance.gameObject);
        // 다른 싱글톤도 마찬가지로 Destroy

       // Cursor.lockState = CursorLockMode.None;
      //  Cursor.visible = true;
        //SceneManager.LoadScene("StartScene");
    }
}
