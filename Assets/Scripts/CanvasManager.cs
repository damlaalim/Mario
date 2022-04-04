using System;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private TextMeshProUGUI textCoin;

    private void Awake()
    {
        Instance = this;
    }

    public void TextTimeChange(float min, float sec)
    {
        textTime.text = string.Format("{0:00}:{1:00}", min, sec);
    }

    public void TextCoinChange()
    {
        textCoin.text = PlayerManager.Instance.coin.ToString();
    }
}