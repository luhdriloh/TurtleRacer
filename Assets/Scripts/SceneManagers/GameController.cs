using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameDoneScreen _gameoverGui;

    public static GameController _gamecontroller;
    public StartCountdown _countDownTimer;
    public ReactionTimeScript _reactionTimeScript;
    public Opponent _opponent;
    public bool _raceDone;

    private bool _raceStarted;
    private float _raceStartTime;

	private void Awake ()
    {
        if (_gamecontroller == null)
        {
            _gamecontroller = this;
        }
        else if (_gamecontroller != this)
        {
            Destroy(this);
        }

        Time.timeScale = 1;
    }

    public void SetRaceToStarted()
    {
        _raceStarted = true;
        _raceStartTime = Time.time;
    }

    public bool HasRaceStarted()
    {
        return _raceStarted;
    }

    public float RaceStartTime()
    {
        return _raceStartTime;
    }

    public void ReactionTime(float time)
    {
        float reactionTime = time - _raceStartTime;
        _reactionTimeScript.SetReactionTime(reactionTime);
    }

    public void FoulStart()
    {
        float timeLeft = _countDownTimer.GetTimeLeft();
        _reactionTimeScript.SetReactionTime(timeLeft);
        _countDownTimer.TurnOnFalseStartLight();
        Gameover();
    }

    public void Gameover()
    {
        Time.timeScale = 0;
        _gameoverGui.TurnOnMessage(string.Empty);
    }

    public void RaceResult(string message)
    {
        _gameoverGui.TurnOnMessage(message);
        _raceDone = true;
    }

    public void Restart()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
