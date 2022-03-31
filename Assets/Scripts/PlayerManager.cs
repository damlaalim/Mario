using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [Range(1, 3)] [SerializeField] private int maxHealth;
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
    }

    private void Start()
    {
        if (PlayerData.Instance.Health == 0)
            PlayerData.Instance.Health = maxHealth;

        HeartCreate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            KillPlayer();
        if (Input.GetKeyDown(KeyCode.L))
            SceneManager.LoadScene("SampleScene");
    }
 
    private void HeartCreate()
    {
        int objectXPosition = 390;

        for (int i = 0; i < PlayerData.Instance.Health; i++)
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

    private void DecreaseHealth()
    {
        PlayerData.Instance.Health--;
    }

    public void KillPlayer()
    {
        Debug.Log("kill player");
        animator.SetBool("IsDead", true);
        playerRigid.velocity = Vector2.up * 15f;
        fallTrigger.enabled = false;
        playerCollider.enabled = false;

        DecreaseHealth();

        if (IsGameOver())
            StartCoroutine(Wait("gameOver", 3f));
        else
            StartCoroutine(Wait("", 2f));
    }

    private bool IsGameOver()
    {
        return PlayerData.Instance.Health == 0;
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