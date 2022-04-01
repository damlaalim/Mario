using System;
using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    public void Run(bool isRight, GameObject enemy, float speed)
    {
        var move = isRight ? Vector3.right : -Vector3.right;
        enemy.transform.position += move * speed * Time.deltaTime;
    }

    public bool MoveChange(bool move)
    {
        return !move;
    }

    public void DeadEnemyCall(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<EnemyController>().DeadEnemy(player);
    }
    
    public void Dead(Rigidbody2D playerRigid, Animator animator, GameObject enemy)
    {
        animator.SetBool("IsDead", true);
        
        enemy.GetComponent<BoxCollider2D>().enabled = false;
    
        playerRigid.velocity = Vector2.up * 15f;
        
        StartCoroutine(EnemyDestroy(enemy));
    }
    
    IEnumerator EnemyDestroy(GameObject enemy)
    {
        yield return new WaitForSeconds(1);
        Destroy(enemy);
    }
}