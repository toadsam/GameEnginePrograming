using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityStandardAssets.CrossPlatformInput; // CrossPlatformInput ���� ��츸 Ȱ��ȭ

public class ForcedReset : MonoBehaviour
{
    void Update()
    {
        // ����: �����̽��ٸ� ������ ���� �ٽ� �ε���
        if (Input.GetKeyDown(KeyCode.R))
        {
            // ���� �� �ٽ� �ε�
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}
