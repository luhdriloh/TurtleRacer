using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerFunMode : MonoBehaviour
{
    public Button _home;
    private RaceCar _raceCar;

    private void Awake()
    {
        _raceCar = GetComponentInChildren<RaceCar>();
        _home.onClick.AddListener(OnHomeScreenButtonPressed);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space))
        {
            _raceCar.ApplyGas(true);
        }
        else
        {
            _raceCar.ApplyGas(false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _raceCar.ShiftUp();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            _raceCar.ShiftDown();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnHomeScreenButtonPressed()
    {
        SceneManager.LoadScene("HomeScreen");
    }

}
