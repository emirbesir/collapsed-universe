using UnityEngine;

public class MoveRightBG : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f; // Speed of the background movement
    private float repeatWidth;
    private Camera mainCamera;

    void Start()
    {
        repeatWidth = GetComponent<BoxCollider2D>().size.x; // Full width of the object
        mainCamera = Camera.main; // Reference to the main camera
    }

    void Update()
    {
        // Move the car to the right
        transform.position += Vector3.right * speed * Time.deltaTime;

        // Calculate the reset position based on the camera's position
        float cameraRightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
        float cameraLeftEdge = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;

        // Check if the car has moved past the camera's right edge plus the repeat width
        if (transform.position.x > cameraRightEdge + repeatWidth)
        {
            // Reset the car's position to the left of the camera's view
            transform.position = new Vector3(cameraLeftEdge - repeatWidth, transform.position.y, transform.position.z);
        }
    }
}
