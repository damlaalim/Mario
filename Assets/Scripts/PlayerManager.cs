using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [Range(1, 5)] [SerializeField] private int maxHealth;
    [SerializeField] private Sprite heart;
    [SerializeField] private GameObject heartParent;
    [SerializeField] private List<GameObject> life;
    [SerializeField] private Rigidbody2D playerRigid;
    [SerializeField] private Collider2D fallTrigger;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Canvas canvasGameOver;

    public Animator princess;
    public GameObject smallCharacter;
    public GameObject bigCharacter;
    
    public bool isDead;
    public bool playerIsBig;
    public int coin;
    public int score;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        coin = 0;
        score = 0;
        
        if (PlayerData.Instance.Health == 0)
            PlayerData.Instance.Health = maxHealth;

        HeartCreate();
    }

    private void Update()
    {
        ShortcutControl();
    }

    private void ShortcutControl()
    {
        if (Input.GetKeyDown(KeyCode.O))
            KillPlayer();
        if (Input.GetKeyDown(KeyCode.L))
            SceneManager.LoadScene("SampleScene");
    }
    
    private void HeartCreate()
    {
        int objectXPosition = 1850;

        for (int i = 0; i < PlayerData.Instance.Health; i++)
        {
            GameObject newObject = new GameObject();
            Image newImage = newObject.AddComponent<Image>();
            newImage.sprite = heart;
            newObject.GetComponent<RectTransform>().SetParent(heartParent.transform);
            newObject.SetActive(true);
            newObject.GetComponent<RectTransform>().position = new Vector3(objectXPosition, 1020, 0);
            newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);
            objectXPosition -= 100;

            life.Add(newObject);
        }
    }

    private void DecreaseHealth()
    {
        PlayerData.Instance.Health--;
    }

    public void KillPlayer()
    {
        isDead = true;
        
        smallCharacter.gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        playerRigid.velocity = Vector2.up * 15f;
        fallTrigger.enabled = false;
        playerCollider.enabled = false;

        DecreaseHealth();

        if (IsGameOver())
            StartCoroutine(SceneLoad("gameOver", 3f));
        else
            StartCoroutine(SceneLoad("", 2f));
    }

    private bool IsGameOver()
    {
        return PlayerData.Instance.Health == 0;
    }

    IEnumerator SceneLoad(string state, float delay = 0)
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