using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float reducedJumpHeight = 1.5f;
    public float jumpSpeed = 60f;
    private float jumpTimer = 61f;

    public Transform groundCheck;
    public float groundDistance = 0.8f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded && jumpTimer > 60)
        {
            bool rayHit = Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.up, 1000, ~(1 << 9));
            float tempJumpHeight = jumpHeight;
            if (rayHit)
            {
                tempJumpHeight = reducedJumpHeight;
                //Debug.Log("Cant jump under roof");

            }
            velocity.y = Mathf.Sqrt(tempJumpHeight * -2f * gravity);
            jumpTimer = 0;
        }
        jumpTimer += jumpSpeed * Time.deltaTime;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}

