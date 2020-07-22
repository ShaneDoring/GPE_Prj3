using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.LogWarning("Attempted to create a second Game Manager.");
            Destroy(this.gameObject);
        }
              
               
    }
    public void StartGame()
    {
        Debug.Log("Game has Started");
    }
}
