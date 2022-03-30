using UnityEngine;

public class PlayerData  : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    public int Health { get; set; }
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);    
        }
        else
        {
            Destroy(gameObject);
        }
    }
}