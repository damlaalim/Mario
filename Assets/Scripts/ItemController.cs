using System;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool moveIsRight = true;
    public bool isMove;

    private void Awake()
    {
        isMove = false;
    }

    private void Update()
    {
        if (!isMove)
            return;
        ItemManager.Instance.Move(moveIsRight, gameObject, speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
            return;

        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.Instance.playerIsBig = true;
            collision.gameObject.GetComponent<Animator>().SetBool("IsGrow", true);

            PlayerManager.Instance.score += GameManager.Instance.redMushroomScore;
            CanvasManager.Instance.TextScoreChange();
            Destroy(gameObject);
        }

        moveIsRight = ItemManager.Instance.MoveChange(moveIsRight);
    }

    private void AnimationFinish()
    {
        var position = transform.position;
        isMove = true;
        transform.SetParent(ItemManager.Instance.collectableParent);
        ItemManager.Instance.collectableParent.transform.position = position;
        GetComponent<Animator>().enabled = false;
    }
}