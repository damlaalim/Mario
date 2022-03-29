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

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (isBreakable)
        {
            case true:
                BlockBreak();
                break;
            case false:
                BlockChange();
                break;
        }
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
        blockSprite.sprite = newBlock;
    }
}
