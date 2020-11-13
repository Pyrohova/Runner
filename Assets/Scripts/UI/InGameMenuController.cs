using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject inGameMenu;

    [SerializeField]
    GameObject youDiedInfo;

    [SerializeField]
    Text coinsText;

    [SerializeField]
    Text livesText;

    [SerializeField]
    Button pauseButton;

    [SerializeField]
    Button okButton;


    public void UpdateCoins(int coins)
    {
        coinsText.text = coins.ToString();
    }

    public void UpdateLives(int lives)
    {
        livesText.text = lives.ToString();
    }

    public void Reset()
    {
        coinsText.text = "0";
        livesText.text = "0";
    }

    //need to show player info what he died
    public void OnPlayerDie()
    {
        youDiedInfo.SetActive(true);
    }

    public void Open()
    {
        inGameMenu.SetActive(true);
        youDiedInfo.SetActive(false);
    }

    public void Close()
    {
        inGameMenu.SetActive(false);
    }

    private void Awake()
    {
        pauseButton.onClick.AddListener(() => {
            GameManager.Instance.PauseGame();
            UIManager.Instance.ShowPauseMenu();
        });

        okButton.onClick.AddListener(() => {
            Close();
            UIManager.Instance.ShowMainMenu();
        });
    }
}
