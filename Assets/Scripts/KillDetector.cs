using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Enemy")
            return;
        
        if (other.gameObject.layer == 9)
        {
            PlayerManager.Instance.KillPlayer();
            return;
        }
        
        if (PlayerManager.Instance.playerIsBig)
        {
            PlayerManager.Instance.bigCharacter.GetComponent<Animator>().SetBool("IsShrinkage", true);
            PlayerManager.Instance.playerIsBig = false;
            return;
        }
        
        PlayerManager.Instance.KillPlayer();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
            EnemyManager.Instance.DeadEnemyCall(col.gameObject, gameObject);
    }
}
