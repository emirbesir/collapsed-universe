using UnityEngine;
using Unity.Cinemachine;

public class CinemachineScreenShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 2.5f;
    [SerializeField] private float shakeIntensity = 3.0f;

    // The Cinemachine Impulse Source component that will generate the shake  
    private CinemachineImpulseSource impulseSource;

    void Awake()
    {
        // Get or add the impulse source component  
        impulseSource = GetComponent<CinemachineImpulseSource>();
        if (impulseSource == null)
        {
            impulseSource = gameObject.AddComponent<CinemachineImpulseSource>();

            // A rumbling earthquake effect
            impulseSource.ImpulseDefinition.ImpulseType = CinemachineImpulseDefinition.ImpulseTypes.Dissipating;
            impulseSource.ImpulseDefinition.ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Rumble;
            impulseSource.ImpulseDefinition.ImpulseDuration = shakeDuration;
            impulseSource.ImpulseDefinition.TimeEnvelope.AttackTime = 0.2f;
            impulseSource.ImpulseDefinition.TimeEnvelope.SustainTime = 1.5f;
            impulseSource.ImpulseDefinition.TimeEnvelope.DecayTime = 0.8f;
            impulseSource.ImpulseDefinition.DissipationRate = 0.3f;
            impulseSource.DefaultVelocity = new Vector3(1.0f, 0.4f, 0f);
        }
    }

    // Call this method to shake the screen  
    public void ShakeScreen()
    {
        impulseSource.GenerateImpulse(shakeIntensity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that entered the trigger is the player  
        if (collision.CompareTag("Player"))
        {
            // Call the ShakeScreen method with the specified intensity
            ShakeScreen();
        }
    }
}
