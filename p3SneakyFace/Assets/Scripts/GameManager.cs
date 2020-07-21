using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
