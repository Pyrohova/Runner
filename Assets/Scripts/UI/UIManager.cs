using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private MainMenuController mainMenu;
    private PauseMenuController pauseMenu;
    private InGameMenuController inGameMenu;
    private ShopMenuController shopMenu;

    public void UpdateCoins(int coins)
    {
        inGameMenu.UpdateCoins(coins);
    }

    public void UpdateLives(int lives)
    {
        inGameMenu.UpdateLives(lives);
    }

    public void ShowMainMenu()
    {
        mainMenu.Open();
        inGameMenu.Close();
        pauseMenu.Close();
        shopMenu.Close();
    }

    public void ShowShop()
    {
        ShopItemsManager.Instance.UpdateItemsInfo();
        shopMenu.Open();
    }

    public void ShowInGameMenu()
    {
        inGameMenu.Open();
        inGameMenu.Reset();
        GameManager.Instance.StartGame();
    }

    public void ShowPauseMenu()
    {
        pauseMenu.Open();
    }

    public void HidePauseMenu()
    {
        pauseMenu.Close();
    }

    public void OnPlayerDie()
    {
        inGameMenu.OnPlayerDie();
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
        mainMenu = GetComponent<MainMenuController>();
        inGameMenu = GetComponent<InGameMenuController>();
        pauseMenu = GetComponent<PauseMenuController>();
        shopMenu = GetComponent<ShopMenuController>();
    }

    private void Awake()
    {
        CreateSingleton();
        InitializeManager();
    }
}
