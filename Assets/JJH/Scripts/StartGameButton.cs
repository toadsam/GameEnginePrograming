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
        Debug.Log("restart 버튼 눌림");
        GameManager.Instance.SetPhase(GamePhase.Title);
        SceneManager.LoadScene("StartScene");
    }

    private void ResetGameState()
    {
        if (GameManager.Instance != null)
        {
          //  GameManager.Instance.SetPhase(GamePhase.Title);
            GameManager.Instance.letterCount = 0;

            // ���� ���� �ʱ�ȭ
            GameManager.Instance.SetDemonDollDead(false);
            GameManager.Instance.SetBookHeadDead(false);
            GameManager.Instance.SetZombieDead(false);
            FullReset();
        }

        // �ٸ� �Ŵ����� �ʱ�ȭ (��: �κ��丮, ���� ��)
        // InventoryManager.Instance?.Reset();
        // ScoreManager.Instance?.Reset();
    }

    public void FullReset()
    {
        Destroy(GameManager.Instance.gameObject);
        Destroy(GameSceneManager.Instance.gameObject); 
            Destroy(EnemyManager.Instance.gameObject);
        // �ٸ� �̱��浵 ���������� Destroy

       // Cursor.lockState = CursorLockMode.None;
      //  Cursor.visible = true;
        //SceneManager.LoadScene("StartScene");
    }
}
