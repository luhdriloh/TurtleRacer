using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDoneScreen : MonoBehaviour
{
    public Text _gameDoneText;
    public Button _restartButton;
    public Button _backToHomeScreenButton;

    private void Start()
    {
        _restartButton.onClick.AddListener(Restart);
        _backToHomeScreenButton.onClick.AddListener(BackToHomeScreen);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void BackToHomeScreen()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }

    public void TurnOnMessage(string text)
    {
        gameObject.SetActive(true);
        _gameDoneText.text = text;
    }
}
