using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private bool isBreakable;
    [SerializeField] private GameObject block;
    [SerializeField] private SpriteRenderer blockSprite;
    [SerializeField] private Sprite newBlock;
    [SerializeField] private float blockUpTime;
    [SerializeField] bool blockIsChange;
    [SerializeField] private GameObject Coin;
    [SerializeField] private GameObject RedMushroom;
    [SerializeField] private InsideItem item;
    private bool playerIsBig;

    private void Awake()
    {
        InsideItemSettings();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        playerIsBig = PlayerManager.Instance.playerIsBig;

        col.GetComponent<Rigidbody2D>().velocity = Vector2.down * 15f;

        BlockTypeSwitch();
        
    }

    private void BlockTypeSwitch()
    {
        if (blockIsChange)
            return;
        
        switch (isBreakable)
        {
            case true when playerIsBig:
                BlockBreak();
                return;
            case false:
                BlockChange();
                break;
        }

        StartCoroutine(BlockMove());
    }
    
    private void BlockBreak()
    {
        StartCoroutine(DestroyAfterDelay(.1f));
        
        IEnumerator DestroyAfterDelay(float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            Destroy(block);
        }

        PlayerManager.Instance.score += GameManager.Instance.blockBreakScore;
        CanvasManager.Instance.TextScoreChange();
    }

    private void BlockChange()
    {
        if (item == InsideItem.Coin)
        {
            CoinSettings();
        }
        else if (item == InsideItem.Mushroom)
        {
            RedMushroomSettings();
        }
        
        blockIsChange = true;
        blockSprite.sprite = newBlock;
    }

    private void CoinSettings()
    {
        PlayerManager.Instance.coin++;
        CanvasManager.Instance.TextCoinChange();
        Coin.GetComponent<Animator>().SetBool("IsBreak", true);
        PlayerManager.Instance.score += GameManager.Instance.CoinScore;
        CanvasManager.Instance.TextScoreChange();
    }

    private void RedMushroomSettings()
    {
        RedMushroom.GetComponent<Animator>().SetBool("IsBreak", true);
        
        RedMushroom.GetComponent<BoxCollider2D>().enabled = true;
        ItemManager.Instance.MoveChange(RedMushroom);
    }

    private void InsideItemSettings()
    {
        if (Coin == null || RedMushroom == null)
            return;
        
        if (item == InsideItem.Mushroom)
        {
            Coin.SetActive(false);
        }
        else if (item == InsideItem.Coin)
        {
            RedMushroom.SetActive(false);
        }
    }
    
    private IEnumerator BlockMove()
    {
        Vector3 startingPos = block.transform.position;
        Vector3 finalPos = block.transform.position + (block.transform.up * .3f);

        float elapsedTime = 0;
        while (elapsedTime < blockUpTime)
        {
            block.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / blockUpTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        elapsedTime = 0;
        while (elapsedTime < blockUpTime)
        {
            block.transform.position = Vector3.Lerp(finalPos, startingPos, (elapsedTime / blockUpTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        block.transform.position = startingPos;

    }
}

public enum InsideItem
{
    Coin,
    Mushroom
};