using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private float time;

    private void Update()
    {
        if (PlayerManager.Instance.isDead)
        {
            return;
        }
        
        if (time > 0)
        {
            time -= Time.deltaTime;
            Counter();
        }
        else
        {
            textTime.text = string.Format("{0:00}:{1:00}", 0, 0);
            PlayerManager.Instance.KillPlayer();
        }
    }

    private void Counter()
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        textTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
