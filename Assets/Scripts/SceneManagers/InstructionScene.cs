using UnityEngine;
using UnityEngine.SceneManagement;


public class InstructionScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("LevelSelectScene");
        }
    }
}
