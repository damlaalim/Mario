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
    private bool playerIsBig;
    [SerializeField] bool blockIsChange;
    [SerializeField] private Animator ItemAnimator;
    
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
    }

    private void BlockChange()
    {
        blockIsChange = true;
        
        PlayerManager.Instance.coin++;
        CanvasManager.Instance.TextCoinChange();
        
        ItemAnimator.SetBool("IsBreak", true);
        blockSprite.sprite = newBlock;
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
