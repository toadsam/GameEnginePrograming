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
        SceneManager.LoadScene("StartScene");
    }
}
