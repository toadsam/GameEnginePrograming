using UnityEngine;

public class UITriggerZone : MonoBehaviour
{
    [Header("UI 오브젝트")]
    public GameObject firstUI;   // 기본으로 뜨는 UI
    public GameObject secondUI;  // 스페이스바 누르면 뜨는 추가 UI

    private bool isPlayerInside = false;

    private void Start()
    {
        firstUI.SetActive(false);
        secondUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            firstUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            firstUI.SetActive(false);
            secondUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.Space))
        {
            secondUI.SetActive(true);
        }
    }
}
