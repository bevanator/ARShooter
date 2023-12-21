using UnityEngine;

public class FreeLookCamera : MonoBehaviour
{
    public float rotationSpeed = 5f;

    private float mouseX;
    private float mouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleRotationInput();
    }

    void HandleRotationInput()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -80f, 80f); // Clamp vertical rotation to prevent camera flipping

        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0); // Rotate the camera
    }
}