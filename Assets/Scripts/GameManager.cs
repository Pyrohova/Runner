using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static bool GameIsPaused = false;

    public event Action OnGameStarted;
    public event Action OnGameEnded;

    //Data from holder + during the level
    private int lives;

    //Get during the level
    private int coinsPerLevel = 0;


    public void StartGame()
    {
        lives = PlayerDataHolder.GetLives();

        OnGameStarted?.Invoke();

        UIManager.Instance.UpdateLives(lives);
    }

    public void PauseGame()
    {
        if (!GameIsPaused)
        {
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    }

    public void ResumeGame()
    {
        if (GameIsPaused)
        {
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
    }

    // End game with saving level result
    // used when player dies
    public void EndGame()
    {
        //Update holder values
        PlayerDataHolder.SetCoins(coinsPerLevel + PlayerDataHolder.GetCoins());
        PlayerDataHolder.SetLives(lives);

        coinsPerLevel = 0;

        OnGameEnded?.Invoke();
    }

    // Called by obstacle prefab
    public void OnPlayerTrappedInObstacle()
    {
        if (lives > 0)
        {
            --lives;
            UIManager.Instance.UpdateLives(lives);
        }
        else
        {
            UIManager.Instance.OnPlayerDie();
            EndGame();
        }
    }

    // Called by coin prefab when player gets on collision coin (item)
    public void AddCoin()
    {
        ++coinsPerLevel;
        UIManager.Instance.UpdateCoins(coinsPerLevel);
    }

    // Called by live prefab when player gets on collision live (item)
    public void AddLive()
    {
        ++lives;
        UIManager.Instance.UpdateLives(lives);
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void InitializeManager()
    {
        lives = PlayerDataHolder.GetLives();
    }

    private void InitializeStartGameValues()
    {
        PlayerDataHolder.SetCoins(100);
        PlayerDataHolder.SetLives(0);

        PlayerDataHolder.SetItem(Item.LiveBooster, 5);
        PlayerDataHolder.SetItem(Item.AccelerationBooster, 0);
    }

    private void Awake()
    {
        CreateSingleton();
        InitializeStartGameValues();
        InitializeManager();
    }


    private void Start()
    {
        UIManager.Instance.ShowMainMenu();
    }
}
