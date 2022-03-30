using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    
    [Range(1,3)] 
    [SerializeField] private int lifeCount;
    [SerializeField] private Sprite heart;
    [SerializeField] private GameObject heartParent;
    [SerializeField] private List<GameObject> life;
    [SerializeField] private Rigidbody2D playerRigid;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D fallTrigger; 
    [SerializeField] private Collider2D playerCollider; 
    [SerializeField] private Canvas canvasGameOver;

    private void Awake()
    {
        Instance = this;
        
        if (PlayerPrefs.GetInt("Health") == 0)
        {
            PlayerPrefs.SetInt("Health", lifeCount);
        }
        else
        {
            lifeCount = PlayerPrefs.GetInt("Health");
        }
    }

    private void Start()
    {
        HeartCreate();
    }

    private void HeartCreate()
    {
        int objectXPosition = 350;
        
        for (int i = 0; i < lifeCount; i++)
        {
            GameObject newObject = new GameObject();
            Image newImage = newObject.AddComponent<Image>();
            newImage.sprite = heart;
            newObject.GetComponent<RectTransform>().SetParent(heartParent.transform);
            newObject.SetActive(true);
            newObject.GetComponent<RectTransform>().position = new Vector3(objectXPosition, 670, 0);
            newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(32, 32);
            objectXPosition -= 50;
            
            life.Add(newObject);
        }
    }

    private void HealthUpdate()
    {
        lifeCount--;
        PlayerPrefs.SetInt("Health", lifeCount);
    }

    public void KillPlayer()
    {
        animator.SetBool("IsDead", true);
        playerRigid.velocity = Vector2.up * 15f;
        fallTrigger.enabled = false;
        playerCollider.enabled = false;
        
        HealthUpdate();

        if (PlayerPrefs.GetInt("Health") <= 0)
        {
            StartCoroutine(Wait("gameOver", 3f));
        }
        else
        {
            StartCoroutine(Wait("", 2f));
        }
    }
    
    IEnumerator Wait(string state, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        
        switch (state)
        {
            case "gameOver":
                canvasGameOver.enabled = true;
                yield return new WaitForSeconds(delay);
                canvasGameOver.enabled = false;
                break;
        }
        SceneManager.LoadScene("SampleScene");
    }
}