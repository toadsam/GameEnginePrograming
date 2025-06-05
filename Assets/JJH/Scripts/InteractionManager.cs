using UnityEngine;
using UnityEngine.UI;

public enum InteractableType
{
    Letter,
    Crowbar,
    Key,
    EscapeDoor
}

public class InteractionManager : MonoBehaviour
{
    public InteractableType interactableType;

    [Header("공통 상호작용 UI")]
    public GameObject interactionUI;

    [Header("Letter 관련")]
    public GameObject[] letterDetails; // detail1 ~ detail5

    [Header("아이템 UI")]
    public GameObject crowbarUI;
    public GameObject keyUI;
    public GameObject dontEscapeUI;

    [Header("사운드")]
    public AudioClip lockSound;
    public AudioClip unlockSound;

    private bool isPlayerNear = false;
    private bool isUsed = false;
    private PlayerState playerState;

    private void Start()
    {
        interactionUI?.SetActive(false);
        playerState = GameObject.FindWithTag("Player")?.GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (!isPlayerNear || isUsed) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (interactableType)
            {
                case InteractableType.Letter:
                    HandleLetterInteraction();
                    break;

                case InteractableType.Crowbar:
                    HandleCrowbarInteraction();
                    break;

                case InteractableType.Key:
                    HandleKeyInteraction();
                    break;

                case InteractableType.EscapeDoor:
                    HandleEscapeInteraction();
                    break;
            }
        }
    }

    private void HandleLetterInteraction()
    {
        int letterCount = playerState.GetLetterCount();

        if (letterCount < letterDetails.Length)
        {
            letterDetails[letterCount].SetActive(true); // 0~4 인덱스
            playerState.CollectLetter(); // 내부에서 count 증가

            Debug.Log($"📩 편지 {letterCount + 1} 획득");

            isUsed = true; // 재상호작용 방지 (동일 Letter)
            Destroy(gameObject); // 실제 편지 오브젝트 제거
        }
    }

    private void HandleCrowbarInteraction()
    {
        if (crowbarUI != null)
            crowbarUI.SetActive(true);

        playerState.ObtainCrowbar();
        Debug.Log("🔧 Crowbar 획득");
        isUsed = true;
        Destroy(gameObject);
    }

    private void HandleKeyInteraction()
    {
        if (keyUI != null)
            keyUI.SetActive(true);

        playerState.ObtainKey();
        Debug.Log("🗝️ 열쇠 획득");
        isUsed = true;
        Destroy(gameObject);
    }

    private void HandleEscapeInteraction()
    {
        if (playerState.HasKey())
        {
            Debug.Log("🔓 탈출 시도: 열쇠 있음");
            PlaySound(unlockSound);
            playerState.CheckEscapeTrigger(); // 탈출 시도
        }
        else
        {
            Debug.Log("🔒 탈출 시도: 열쇠 없음");
            PlaySound(lockSound);
            if (dontEscapeUI != null)
                dontEscapeUI.SetActive(true);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactionUI?.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactionUI?.SetActive(false);
        }
    }
}
