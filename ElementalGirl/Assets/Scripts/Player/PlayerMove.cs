using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private PlayerInput input;
    [SerializeField] private Rigidbody rb;

    [Header("Player")]
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform playerHead;

    [Header("Status")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;


    [Header("Camera Rotation")]
    [SerializeField] private float mouseSensitivity = 200;
    [SerializeField] private float maxXRotation = 30f;
    [SerializeField] private float minXRotation = -80f;


    private float xRotation = 0f;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CamaraRotateToCursor();
        Move();
        Jump();
    }

    private void CamaraRotateToCursor()
    {
        float yOffset = input.MouseX * mouseSensitivity * Time.deltaTime;
        float xOffset = input.MouseY * mouseSensitivity * Time.deltaTime;

        xRotation -= xOffset;
        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);

        // 나 자신은 위 아래 회전만 함
        playerHead.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 몸체 자체는 양옆 회전만 함
        playerBody.Rotate(Vector3.up * yOffset);
    }

    private void Move()
    {
        float h = input.Horizontal;
        float v = input.Vertical;

        Vector3 dir = playerBody.right * h + playerBody.forward * v;
        rb.MovePosition(playerBody.transform.position + dir.normalized * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (input.Jump == false)
            return;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
