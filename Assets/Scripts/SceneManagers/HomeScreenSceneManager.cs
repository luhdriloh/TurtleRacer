using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeScreenSceneManager : MonoBehaviour
{
    public Text _startGameText;
    private Color _startcolor;

    private void Start()
    {
        _startcolor = _startGameText.color;
    }

    private void Update ()
    {
        _startGameText.color = Color.Lerp(_startcolor, Color.gray, Mathf.PingPong(Time.time, 1));

        if (Input.anyKey)
        {
            SceneManager.LoadScene("LevelSelectScene");
        }
    }
}
