using System;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    public Transform collectableParent;
    
    
    private void Awake()
    {
        Instance = this;
    }

    public void Move(bool isRight, GameObject item, float speed)
    {
        var move = isRight ? Vector3.right : -Vector3.right;
        item.transform.position += move * speed * Time.deltaTime;
    }

    // public void MoveChange(GameObject item)
    // {
    //     item.transform.SetParent(collectableParent);
    //     item.GetComponent<ItemController>().isMove = true;
    // }
    
    public bool MoveChange(bool isRight) => !isRight;
}