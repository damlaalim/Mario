using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance { get; private set; }
    [SerializeField] private Animator animator;
    public float speed;
    private Rigidbody2D _rigidbody;
    private bool moveRight;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        moveRight = false;
        _rigidbody = GetComponent<Rigidbody2D>(); 
    }
    
    private void Update()
    {
        Run();
    }

    private void Run()
    {
        var move = moveRight ? Vector3.right : -Vector3.right;
        transform.position += move * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        moveRight = !moveRight;
    }
    
    public void Dead(Rigidbody2D playerRigid)
    {
        animator.SetBool("IsDead", true);
        
        GetComponent<BoxCollider2D>().enabled = false;

        playerRigid.velocity = Vector2.up * 15f;
        speed = 0f;
        StartCoroutine(EnemyDestroy());
    }
    
    IEnumerator EnemyDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}