using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Vector2 movementInput;
    public int id;

    [SerializeField] private Transform groundPos;


    private Rigidbody rb;
    private Animator animator;
    private GameObject opponent;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        GameObject[] opponents = GameObject.FindGameObjectsWithTag("Player");
        foreach (var character in opponents)
        {
            if (character.GetComponent<PlayerController>().id == id) continue;
            opponent = character;
            return;
        }
    }

    private void Update()
    {
        Move();
        if (movementInput.y > 0.2f && isGrounded()) Jump();
        print(isGrounded());
        animator.SetFloat("velocityY", rb.velocity.y);
    }

    private void Move()
    {
        rb.velocity = new Vector2(movementInput.x * speed * Time.deltaTime, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetTrigger("Jump");
    }

    private bool isGrounded()
    {
        if (Physics.Raycast(groundPos.position, Vector3.down, 0.1f)) return true;
        return false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        animator.SetFloat("movementX", movementInput.x);
    }
}
