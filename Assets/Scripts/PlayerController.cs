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
    [SerializeField] float jumpHeight = 5;
    [SerializeField] float buttonTime = 0.5f;
    [SerializeField] float cancelRate = 100;
    [SerializeField] Animator animator;

    private float jumpTime;
    private bool jumpCancelled;
    private bool dontMove = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (PlayerManager.Instance.isDead || dontMove)
            return;
        
        Run();
        ChangeWalkDirection();
        Jump();
    }

    private void FixedUpdate()
    {
        if(jumpCancelled && animator.GetBool("IsJumping") && _rigidbody.velocity.y > 0)
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
            jumpCancelled = false;
            jumpTime = 0;

            _isGrounded = false;
        }
        
        if (animator.GetBool("IsJumping"))
        {
            animator.SetBool("IsJumping", true);

            jumpTime += Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpCancelled = true;
            }
            if (jumpTime > buttonTime)
            {
                animator.SetBool("IsJumping", false);
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

    private void CharacterGrow()
    {
        var position = PlayerManager.Instance.smallCharacter.transform.position;
        PlayerManager.Instance.smallCharacter.SetActive(false);
        PlayerManager.Instance.bigCharacter.transform.position = position;
        PlayerManager.Instance.bigCharacter.SetActive(true);
    }

    private void CharacterShrinkage()
    {
        var position = PlayerManager.Instance.bigCharacter.transform.position;
        PlayerManager.Instance.bigCharacter.SetActive(false);
        PlayerManager.Instance.smallCharacter.transform.position = position;
        PlayerManager.Instance.smallCharacter.SetActive(true);
    }

    private void CharacterDontMove()
    {
        dontMove = true;
    }

    private void CharacterMove()
    {
        dontMove = false;
    }

    private void UnTouchable()
    {
        PlayerManager.Instance.bigCharacter.GetComponent<Rigidbody2D>().gravityScale = 0;
        PlayerManager.Instance.smallCharacter.GetComponent<Rigidbody2D>().gravityScale = 0;
        PlayerManager.Instance.bigCharacter.GetComponent<BoxCollider2D>().enabled = false;
        PlayerManager.Instance.smallCharacter.GetComponent<BoxCollider2D>().enabled = false;
    }
    
    private void Touchable()
    {
        PlayerManager.Instance.bigCharacter.GetComponent<Rigidbody2D>().gravityScale = 5;
        PlayerManager.Instance.smallCharacter.GetComponent<Rigidbody2D>().gravityScale = 5;
        PlayerManager.Instance.bigCharacter.GetComponent<BoxCollider2D>().enabled = true;
        PlayerManager.Instance.smallCharacter.GetComponent<BoxCollider2D>().enabled = true;
    }
}