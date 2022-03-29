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
       PlayerManager.Instance.KillPlayer();
    }
}
