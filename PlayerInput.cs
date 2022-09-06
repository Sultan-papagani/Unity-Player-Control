using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] MotorControl motor;

    [Header("Settings")]
    [SerializeField] float Sensvity;

    void Start()
    {
        motor.SetupCamera();
    }

    void Update()
    {
        float x = Input.GetAxis("Mouse X") * Sensvity;
        float y = Input.GetAxis("Mouse Y") * Sensvity;
        motor.RotateViewPoint(x, y);

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        motor.Move(move);

        bool jumping = Input.GetKeyDown(KeyCode.Space);
        motor.ApplyGravity(jumping);
    }
}
