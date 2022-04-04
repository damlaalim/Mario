using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private float time;
    public int CoinScore;
    public int redMushroomScore;
    public int blockBreakScore;
    public int mushroomScore;
    
    private void Awake()
    {
        Instance = this;
    }

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
            CanvasManager.Instance.TextTimeChange(0,0);
            PlayerManager.Instance.KillPlayer();
        }
    }

    private void Counter()
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        CanvasManager.Instance.TextTimeChange(minutes, seconds);
    }
}
