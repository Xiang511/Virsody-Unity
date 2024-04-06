using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float moveSpeed = 0.5f; // 移動速度
    public float rotationSpeed = 0.5f; // 轉頭速度
    public float jumpForce = 0.5f; // 跳躍力度
    private CharacterController characterController;
    private Vector3 moveDirection;
    private float ySpeed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleMouseLook();
    }

    void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 計算移動向量
        moveDirection = (transform.TransformDirection(Vector3.forward) * verticalInput) + (transform.TransformDirection(Vector3.right) * horizontalInput);
        moveDirection.Normalize();

        // 處理跳躍
        HandleJump();

        // 應用重力效果
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // 向下移動時確保不會無限墜落
        if (characterController.isGrounded && ySpeed < 0)
        {
            ySpeed = -0.5f;
        }

        // 將移動向量應用到角色的位置
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime + new Vector3(0, ySpeed, 0));
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        // 將轉頭量應用到角色的旋轉
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleJump()
    {
        if (characterController.isGrounded)
        {
            // 按下空白鍵時進行跳躍
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpForce;
            }
        }
    }
}

