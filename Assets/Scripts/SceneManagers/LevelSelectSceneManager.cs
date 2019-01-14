using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectSceneManager : MonoBehaviour
{
    public GameObject _winStar;
    public List<GameObject> _experience;

    public LevelSelectInformation _levelSelectInformation;
    public Button _backOneLevelButton;
    public Button _nextLevelButton;
    public Button _playButton;
    public Text _opponentName;
    public Text _winTime;
    public SpriteRenderer _car;

    public List<LevelData> _listOfLevels;

    private int _selectedLevel;
    private int _numberOfLevels;

    private void Start()
    {
        // set up level information
        _backOneLevelButton.onClick.AddListener(BackOneLevelButtonClicked);
        _nextLevelButton.onClick.AddListener(NextLevelButtonClicked);
        _playButton.onClick.AddListener(OnPlayButtonClicked);

        _selectedLevel = 0;
        _numberOfLevels = _listOfLevels.Count;

        UpdateLevelInformation();
    }

    private void NextLevelButtonClicked()
    {
        _selectedLevel = (_selectedLevel + 1) % _numberOfLevels;
        UpdateLevelInformation();
    }

    private void BackOneLevelButtonClicked()
    {
        _selectedLevel = (_selectedLevel + _numberOfLevels - 1) % _numberOfLevels;
        UpdateLevelInformation();
    }

    private void OnPlayButtonClicked()
    {
        _levelSelectInformation.SetLevelInformation(_listOfLevels[_selectedLevel]);
        SceneManager.LoadScene("GameScene");
    }

    private void UpdateLevelInformation()
    {
        LevelData data = _listOfLevels[_selectedLevel];
        _car.color = data._opponentCarColor;
        _opponentName.text = data._opponentName;

        if (PlayerPrefs.GetFloat(data._difficulty.ToString(), 0) > 0)
        {
            _winTime.text = PlayerPrefs.GetFloat(data._difficulty.ToString()).ToString("0.000") + "s";
        }
        else
        {
            _winTime.text = string.Empty;
        }

        if (PlayerPrefs.GetInt(data._difficulty.ToString() + "win", 0) > 0)
        {
            _winStar.SetActive(true);
        }
        else
        {
            _winStar.SetActive(false);
        }

        for (int i = 0; i < _experience.Count; i++)
        {
            _experience[i].SetActive(data._difficulty > i);
        }
    }
}
