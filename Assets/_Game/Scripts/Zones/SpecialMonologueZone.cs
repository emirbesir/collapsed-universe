using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class SpecialMonologueZone : MonoBehaviour
{
    [Header("Cutscene Settings")]
    [SerializeField] private bool playOnce = true;
    [SerializeField] private CinemachineCamera cutsceneCamera;
    [SerializeField] private float blendDuration = 1.5f;
    [SerializeField] private float cutsceneDuration = 2f;
    [SerializeField] private bool freezePlayerDuringCutscene = true;
    [SerializeField] private int cutscenePriority = 15;

    private bool hasPlayed = false;
    private int originalPriority;
    private CinemachineBrain cinemachineBrain;

    private void Start()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

        if (cutsceneCamera == null)
        {
            Debug.LogWarning("No cutscene camera set for " + gameObject.name);
        }
        else
        {
            // Store the original priority so we can restore it later
            originalPriority = cutsceneCamera.Priority;

            // Initially set to a low priority so it doesn't activate
            cutsceneCamera.Priority = 0;
        }

        if (cinemachineBrain == null)
        {
            Debug.LogWarning("Cinemachine Brain not found on main camera!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playOnce && hasPlayed)
                return;

            StartCoroutine(PlayCutscene(collision.gameObject));
            hasPlayed = true;
        }
    }

    private IEnumerator PlayCutscene(GameObject player)
    {
        // Freeze player if needed
        if (freezePlayerDuringCutscene)
        {
            // Disable player movement script
            PlayerController playerMovement = player.GetComponent<PlayerController>();
            playerMovement.StopMoving();
            playerMovement.enabled = false;

            Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
            playerRigidbody.linearVelocity = Vector2.zero;
            playerRigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        // Activate cutscene camera if it exists
        if (cutsceneCamera != null)
        {
            // Set cutscene camera to high priority to activate it
            cutsceneCamera.Priority = cutscenePriority;

            // Wait for the blend to complete
            if (cinemachineBrain != null)
            {
                // Wait a little extra to ensure the blend begins
                yield return new WaitForSeconds(0.1f);

                // If a blend is in progress, wait for it to complete
                while (cinemachineBrain.IsBlending)
                {
                    yield return null;
                }
            }
            else
            {
                // If no brain reference, just wait the blend duration
                yield return new WaitForSeconds(blendDuration);
            }

            // Hold at cutscene camera
            yield return new WaitForSeconds(cutsceneDuration);

            // Restore original camera by resetting priority
            cutsceneCamera.Priority = originalPriority;

            // Wait for blend back to complete
            if (cinemachineBrain != null)
            {
                // Wait a little extra to ensure the blend begins
                yield return new WaitForSeconds(0.1f);

                // If a blend is in progress, wait for it to complete
                while (cinemachineBrain.IsBlending)
                {
                    yield return null;
                }
            }
            else
            {
                // If no brain reference, just wait the blend duration
                yield return new WaitForSeconds(blendDuration);
            }
        }

        // Unfreeze player
        if (freezePlayerDuringCutscene)
        {
            // Re-enable player movement script
            PlayerController playerMovement = player.GetComponent<PlayerController>();
            playerMovement.enabled = true;

            // Restore Rigidbody2D settings
            Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
            playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}