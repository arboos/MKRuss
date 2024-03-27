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
    
    
    private Rigidbody rb;
    private GameObject opponent;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GameObject[] opponents = GameObject.FindGameObjectsWithTag("Player");
        foreach (var character in opponents)
        {
            if(character.GetComponent<PlayerController>().id == id) continue;
            opponent = character;
            return;
        }
    }

    private void Update()
    {
        Move();
        if (movementInput.y > 0.2f && isGrounded()) Jump();
    }

    private void Move()
    {
        rb.velocity = new Vector2(movementInput.x * speed * Time.deltaTime, rb.velocity.y);
    }
    
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private bool isGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1f)) return true;
        return false;
    }

    public void OnMove(InputAction.CallbackContext context) => movementInput = context.ReadValue<Vector2>();
}
