using System;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 로딩을 위해 추가
using UnityStandardAssets.CrossPlatformInput;

public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // 리셋 버튼 입력되었을 때
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            // 현재 씬 비동기 로딩
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}
