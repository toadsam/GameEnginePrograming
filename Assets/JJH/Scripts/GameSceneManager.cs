using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneManager
{
    public static void LoadScene(string sceneName)
    {
        Debug.Log($"📥 씬 전환: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadEscapeScene()
    {
        GameManager.Instance.SetPhase(GamePhase.GameOver);
        LoadScene("EscapeScene");
    }

    public static void LoadDeadEndingScene()
    {
        GameManager.Instance.SetPhase(GamePhase.GameOver);
        LoadScene("DeadEndingScene");
    }

    public static void LoadHiddenEndingScene()
    {
        GameManager.Instance.SetPhase(GamePhase.GameOver);
        LoadScene("HiddenEndingScene");
    }

    public static void LoadTitleScene()
    {
        GameManager.Instance.SetPhase(GamePhase.Title);
        LoadScene("TitleScene");
    }

    public static void LoadMainGameScene()
    {
        GameManager.Instance.SetPhase(GamePhase.Ingame);
        LoadScene("MainGameScene");
    }
}
