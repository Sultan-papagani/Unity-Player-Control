using UnityEngine;

public class MotorControl : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] CharacterController character;

    [Header("Transforms")]
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform cameraMountPoint;

    [Header("Player Stats")]
    [SerializeField] float PlayerSpeed;
    [SerializeField] float JumpHeight;
    [SerializeField] float GravityValue;

    [Header("Camera Settings")]
    [SerializeField] float ClampCameraMin;
    [SerializeField] float ClampCameraMax;

    float current_gravity;
    float camera_rotate_yAxis;
    Transform playerCamera;

    public void SetupCamera(Transform camera = null)
    {
        if (camera == null)
        {
            camera = Camera.main.transform;
        }

        playerCamera = camera;
        playerCamera.SetParent(cameraMountPoint.transform);
        playerCamera.localPosition = Vector3.zero;
    }

    public void Move(Vector3 vec)
    {
        Vector3 final = transform.TransformDirection(vec * PlayerSpeed);
        character.Move(final * Time.deltaTime);
    }

    public void RotateViewPoint(float x, float y)
    {
        transform.Rotate(Vector3.up * x);

        camera_rotate_yAxis += y;
        camera_rotate_yAxis = Mathf.Clamp(camera_rotate_yAxis, ClampCameraMin, ClampCameraMax);
        playerCamera.localRotation = Quaternion.Euler(-camera_rotate_yAxis, 0f, 0f);
    }

    public void ApplyGravity(bool jump)
    {
        bool yerde = YerdeMi();
        if (yerde) { current_gravity = 0f; }

        if (jump && yerde)
        {
            current_gravity += Mathf.Sqrt(JumpHeight * 3.0f * GravityValue);
        }

        current_gravity -= GravityValue * Time.deltaTime;

        character.Move(transform.TransformDirection(Vector3.up * current_gravity * Time.deltaTime));
    }

    public bool YerdeMi()
    {
        return Physics.Raycast(groundCheckPoint.position, Vector3.down, 0.1f);
    }

}
