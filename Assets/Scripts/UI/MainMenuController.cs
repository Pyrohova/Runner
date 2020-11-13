using UnityEngine;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    Button startButton;

    [SerializeField]
    Button shopButton;

    public void Open()
    {
        mainMenu.SetActive(true);
    }

    public void Close()
    {
        mainMenu.SetActive(false);
    }

    private void Awake()
    {
        startButton.onClick.AddListener(() => {
            UIManager.Instance.ShowInGameMenu();
            Close();
        });

        shopButton.onClick.AddListener(() => {
            UIManager.Instance.ShowShop();
            Close();
        });
    }

}
