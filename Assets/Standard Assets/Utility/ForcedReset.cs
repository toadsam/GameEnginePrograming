using System;
using UnityEngine;
using UnityEngine.SceneManagement; // �� �ε��� ���� �߰�
using UnityStandardAssets.CrossPlatformInput;

public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // ���� ��ư �ԷµǾ��� ��
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            // ���� �� �񵿱� �ε�
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}
