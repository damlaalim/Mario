using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] float speed;
    private Rigidbody2D _rigidbody;
    private bool moveRight;
    private bool isDead;
    
    private void Start()
    {
        moveRight = false;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDead)
            return;
        
        EnemyManager.Instance.Run(moveRight, gameObject, speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        moveRight = EnemyManager.Instance.MoveChange(moveRight);
    }

    public void DeadEnemy(GameObject player)
    {
        isDead = true;
        EnemyManager.Instance.Dead(player.GetComponent<Rigidbody2D>(), animator, gameObject);
    }
}