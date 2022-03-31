using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private bool _isGrounded;
    private float _jumpTimeCounter;
    private Vector3 _velocity;
    private Rigidbody2D _rigidbody;
    
    [SerializeField] float speed;
    [SerializeField] Animator animator;
    
    [SerializeField] float jumpHeight = 5;
    [SerializeField] float buttonTime = 0.5f;
    [SerializeField] float cancelRate = 100;
    
    private float jumpTime;
    private bool jumping;
    private bool jumpCancelled; 

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        // _jumpTimeCounter = jumpTime;
    }

    private void Update()
    { 
        Run();
        ChangeWalkDirection();
        Jump();
    }

    private void FixedUpdate()
    {
        if(jumpCancelled && jumping && _rigidbody.velocity.y > 0)
        {
            _rigidbody.AddForce(Vector2.down * cancelRate);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor")
        {
            animator.SetBool("IsJumping", false);
            _isGrounded = true;
        }
    } 
    
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            animator.SetBool("IsJumping", true);

            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * _rigidbody.gravityScale));
            _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumping = true;
            jumpCancelled = false;
            jumpTime = 0;

            _isGrounded = false;
        }
        
        if (jumping)
        {
            animator.SetBool("IsJumping", true);

            jumpTime += Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpCancelled = true;
            }
            if (jumpTime > buttonTime)
            {
                jumping = false;
            }
            _isGrounded = false;
        }
        
        if(animator.GetBool("IsJumping") && _isGrounded)
            animator.SetBool("IsJumping", false);
    }

    private void Run()
    { 
        _velocity = new Vector3(Input.GetAxis("Horizontal"), 0f);
        transform.position += _velocity * speed * Time.deltaTime;
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
    }

    private void ChangeWalkDirection()
    {
        if (Input.GetAxisRaw("Horizontal") == -1)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        if (Input.GetAxisRaw("Horizontal") == 1)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}