using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static EnemyManager Instance { get; private set; }

    [Header("���� ��ġ�� ���� ���۷���")]
    public MonsterAI dollMonster;
    public MonsterAI bookheadMonster;

    void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // ����: ���� �������ڸ��� �� �� OFF
        if (dollMonster != null)
            dollMonster.DisableChaseAndAttack();

        if (bookheadMonster != null)
            bookheadMonster.DisableChaseAndAttack();
    }

    // UI ��ư�̳� �̺�Ʈ���� ȣ���� �� �ֵ��� ���� �޼���
    public void ToggleDollBehavior(bool on)
    {
        if (dollMonster != null)
            dollMonster.SetChaseAndAttackEnabled(on);
    }

    public void ToggleBookheadBehavior(bool on)
    {
        if (bookheadMonster != null)
            bookheadMonster.SetChaseAndAttackEnabled(on);
    }
}
