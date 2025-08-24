using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float cameraHeight;
    [SerializeField] private Transform target;

    public Transform Target { get => target; set { target = value; } }

    private float verticalRotation;
    private float horizontalRotation;

    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + new Vector3(0, cameraHeight, 0);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        verticalRotation -= mouseY * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -70f, 70f);

        horizontalRotation += mouseX * mouseSensitivity;

        transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
    }
}