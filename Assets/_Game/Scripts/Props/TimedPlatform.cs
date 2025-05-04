using System.Collections;
using UnityEngine;

public class TimedPlatform : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f; // Time before the platform disappears

    private void OnEnable()
    {
        // Start the coroutine to destroy the platform after a certain time
        StartCoroutine(DeactivateAfterTime());
    }

    private IEnumerator DeactivateAfterTime()
    {
        // Wait for the specified lifetime
        yield return new WaitForSeconds(lifeTime);
        // Deactivate the platform
        gameObject.SetActive(false);
    }

}
