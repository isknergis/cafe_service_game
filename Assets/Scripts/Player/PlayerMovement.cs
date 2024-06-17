using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;

    private Rigidbody rb;
    private Vector3 input;
    private float mouseInput;
    private void Awake()
    {
            rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        GetInput();
            RotateChar();
    }
    private void RotateChar()
    {
        if (Input.GetMouseButton(1))
        {
            mouseInput = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up * mouseInput * sensitivity);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void GetInput()
    {
        input.z = Input.GetAxis("Vertical");
        input.x=   Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.TransformDirection(input) * speed * Time.fixedDeltaTime);
    }
}
