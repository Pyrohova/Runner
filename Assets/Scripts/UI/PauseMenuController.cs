using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    Button resumeButton;

    public void Open()
    {
        pauseMenu.SetActive(true);
    }

    public void Close()
    {
        pauseMenu.SetActive(false);
    }

    private void Start()
    {
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.ResumeGame();
            UIManager.Instance.HidePauseMenu();
        });
    }

}
