using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string gameState = "Start Screen";
    public GameObject titleScreen;
    public GameObject gameUI;
    public GameObject player;
    public GameObject playerPrefab;
    public GameObject playerSpawnPoint;
    public GameObject playerDeathScreen;
    public GameObject gameOverScreen;
    public GameObject enemy;
    public GameObject enemyPrefab;
    public GameObject enemySpawnPoint;
    public int lives = 3;

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

    public void ChangeState (string newState)
    {
        gameState = newState;
    }
    public void StartGame()
    {
        Debug.Log("Game has Started");
        ChangeState("Initialize Game");
    }


    private void Update()
    {
        if (gameState=="Start Screen")
        {
            //Do the State Behaviour
            StartScreen();
            //Check for Transitions
            //this transitions on a button click
        }
         else if (gameState=="Initialize Game")
        {
            //Do the State Behaviour
            InitializeGame();
            //Check for Transitions
            ChangeState("Spawn Player");
        }
        else if (gameState=="Spawn Player")
        {
            //Do the State Behaviour
            SpawnPlayer();
            SpawnEnemy();
            //Check for Transitions
            ChangeState("Gameplay");
        }
        else if (gameState == "Gameplay")
        {
            //Do the State Behaviour
            Gameplay();

            //Check for Transitions
            if (player==null && lives > 0)
            {
                ChangeState("Player Death");
            }
            else if(player==null && lives <= 0)
            {
                ChangeState("Game Over");
            }

        }
        else if (gameState=="Player Death")
        {
            //Do the State Behaviour
            PlayerDeath();
            //Check for Transitions
            if (Input.anyKeyDown)
            {
                ChangeState("Spawn Player");
            }
        }
        else if(gameState=="Game Over")
        {
            //Do the State Behaviour
            GameOver();
            //Check for Transitions
            if (Input.anyKeyDown)
            {
                ChangeState("Start Screen");
            }
        }
        else
        {
            Debug.LogWarning("The Game Manager tried to change to a non existing state" + gameState);
        }
    }
    public void StartScreen()
    {
        //Show the Menu
        if (!titleScreen.activeSelf)
        {
            titleScreen.SetActive(true);
        }
        
    }

    public void InitializeGame()
    {
        //Reset all variables
        //TODO: Rset variables in Initialize Game

        //Turn off menu
        titleScreen.SetActive(false);
        //Turn on Game UI
        gameUI.SetActive(true);
    }

    public void SpawnPlayer()
    {
        //Add the player character to the world
        player = Instantiate(playerPrefab, playerSpawnPoint.transform.position, Quaternion.identity);
    }

    public void SpawnEnemy()
    {
        enemy = Instantiate(enemyPrefab, enemySpawnPoint.transform.position, Quaternion.identity);
    }

    public void Gameplay()
    {

    }

    public void PlayerDeath()
    {
        if (!playerDeathScreen.activeSelf)
        {
            playerDeathScreen.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (gameUI.activeSelf)
        {
            gameUI.SetActive(false);
        }
        if (!gameOverScreen.activeSelf)
        {
            gameOverScreen.SetActive(true);
        }
    }
}
