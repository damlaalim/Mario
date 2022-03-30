using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D _rigidbody;
    private bool moveRight;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        moveRight = !moveRight;
    }

    void Start()
    {
        moveRight = false;
        _rigidbody = GetComponent<Rigidbody2D>(); 
    }
    
    void Update()
    {
        Run();
    }

    void Run()
    {
        var move = moveRight ? Vector3.right : -Vector3.right;
        transform.position += move * speed * Time.deltaTime;
    }
}