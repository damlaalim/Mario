using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool _isGrounded;
    private float _jumpTimeCounter;
    private Vector3 _velocity;
    private Rigidbody2D _rigidbody;

    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float checkRadius;
    [SerializeField] float jumpTime;
    [SerializeField] Transform feetPos;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask whatIsGround;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    { 
        Run();
        ChangeWalkDirection();
        Jump();
    }

    private void Jump()
    {
        _isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsJumping", true);
            _jumpTimeCounter = jumpTime;
            _rigidbody.velocity = Vector2.up * jumpForce; 
        }

        if (Input.GetKey(KeyCode.Space) && animator.GetBool("IsJumping"))
        {
            if (_jumpTimeCounter > 0)
            {
                _rigidbody.velocity = Vector2.up * jumpForce;
                _jumpTimeCounter -= Time.deltaTime;
                animator.SetBool("IsJumping", true);
            }
        }

        if (animator.GetBool("IsJumping") && Mathf.Approximately(_rigidbody.velocity.y, 0))
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