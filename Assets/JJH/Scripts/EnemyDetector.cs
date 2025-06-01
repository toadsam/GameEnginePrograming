using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [Header("시야 설정")]
    public float viewAngle = 90f;     // 부채꼴 각도
    public float viewRange = 10f;     // 시야 거리

    [Header("손전등 참조")]
    public Transform playerTransform;
    public Transform playerFlashlightObject;
    private PlayerFlashlight flashlight;

    private void Start()
    {
        flashlight = playerFlashlightObject.GetComponent<PlayerFlashlight>();
        if (flashlight == null)
            Debug.LogWarning("⚠️ PlayerFlashlight 스크립트 찾지 못함");
    }

    private void Update()
    {
        if (flashlight == null || playerTransform == null) return;

        bool seesPlayer = IsPlayerInFOV();
        bool seesFlashlight = IsLightConeInFOV();

        if (seesPlayer)
            Debug.Log("🔴 조건1: 적이 플레이어를 직접 시야로 감지");

        if (seesFlashlight)
            Debug.Log("🔦 조건2: 적이 손전등의 빛 범위를 감지");

        if (seesPlayer || seesFlashlight)
        {
            Debug.Log("🎯 적이 감지 조건을 만족! 추적 시작");
            GameManager.Instance.ToggleDollBehavior(true);

        }
            

    }

    private bool IsPlayerInFOV()
    {
        Vector3 toPlayer = playerTransform.position - transform.position;
        float distToPlayer = toPlayer.magnitude;

        if (distToPlayer > viewRange) return false;

        float angleToPlayer = Vector3.Angle(transform.forward, toPlayer.normalized);
        return angleToPlayer <= viewAngle * 0.5f;
    }

    private bool IsLightConeInFOV()
    {
        Vector3 coneOrigin = flashlight.GetConeOrigin();
        Vector3 coneDir = flashlight.GetConeDirection();
        float coneRange = flashlight.GetConeRange();
        float coneAngle = flashlight.GetConeAngle(); // 반각도

        // 적의 시야 안에서, cone 범위가 겹치는지 판단
        int sampleCount = 10;
        for (int i = 0; i <= sampleCount; i++)
        {
            float t = (float)i / sampleCount;
            Vector3 samplePoint = coneOrigin + coneDir * coneRange * t;

            Vector3 toSample = samplePoint - transform.position;
            float dist = toSample.magnitude;
            float angle = Vector3.Angle(transform.forward, toSample.normalized);

            if (dist <= viewRange && angle <= viewAngle * 0.5f)
                return true; // 적의 시야 안에 빛 cone의 일부가 들어옴
        }

        return false;
    }

    // ✅ 디버깅용 FOV 시각화
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;

        float halfAngle = viewAngle * 0.5f;

        Vector3 rightDir = Quaternion.Euler(0, halfAngle, 0) * forward;
        Vector3 leftDir = Quaternion.Euler(0, -halfAngle, 0) * forward;

        Gizmos.DrawLine(origin, origin + rightDir * viewRange);
        Gizmos.DrawLine(origin, origin + leftDir * viewRange);
    }
}