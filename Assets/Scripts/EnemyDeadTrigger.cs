using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject enemy;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetBool("IsDead", true);
        enemy.GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        other.GetComponent<Rigidbody2D>().velocity = Vector2.up * 15f;
        enemy.GetComponent<EnemyController>().speed = 0f;
        StartCoroutine(EnemyDestroy());
    }

    IEnumerator EnemyDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(enemy);
    }
}