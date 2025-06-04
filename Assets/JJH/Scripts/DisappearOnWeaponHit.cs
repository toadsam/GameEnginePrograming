using System.Collections;
using UnityEngine;

public class DisappearOnWeaponHit : MonoBehaviour
{
    [Header("맞으면 3초 후 사라질 오브젝트")]
    public GameObject targetObject;

    [Header("맞는 태그")]
    public string weaponTag = "Weapon";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(weaponTag))
        {
            StartCoroutine(RemoveAfterDelay(3f));
        }
    }

    private IEnumerator RemoveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (targetObject != null)
            Destroy(targetObject);
    }
}
