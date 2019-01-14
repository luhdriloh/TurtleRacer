using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeScreenSceneManager : MonoBehaviour
{
    public Button _startButton;
    public Button _instructionsButton;
    public Button _funModeButton;

    private void Start()
    {
        _startButton.onClick.AddListener(ToLevelSelectMenu);
        _instructionsButton.onClick.AddListener(ToInstructionMenu);
        _funModeButton.onClick.AddListener(FunMode);
    }

    private void ToLevelSelectMenu()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }

    private void ToInstructionMenu()
    {
        SceneManager.LoadScene("InstructionScene");
    }

    private void FunMode()
    {
        SceneManager.LoadScene("FunScene");
    }
}
