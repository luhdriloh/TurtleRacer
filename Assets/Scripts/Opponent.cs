using UnityEngine;

public class Opponent : MonoBehaviour
{
    public LevelData _opponentInformation;

    private RaceCar _raceCar;
    private SpriteRenderer _spriteRenderer;
    private float _nextGearShift;
    private float _reactionTime;
    private bool _started;

    private void Start()
    {
        SetOpponentStats();
        _raceCar = GetComponentInChildren<RaceCar>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.color = _opponentInformation._opponentCarColor;
        _nextGearShift = Random.Range(_opponentInformation._minGearShift, _opponentInformation._maxGearShift);
        _reactionTime = Random.Range(_opponentInformation._reactionTimeMin, _opponentInformation._reactionTimeMax);
    }

    private void Update()
    {
        float timeSinceRaceStarted = Time.time - GameController._gamecontroller.RaceStartTime();
        if (GameController._gamecontroller.HasRaceStarted() && timeSinceRaceStarted >= _reactionTime && _started == false)
        {
            _raceCar.ApplyGas(true);
            _started = true;
        }

        if (_raceCar.GetCurrentRpm() >= _nextGearShift)
        {
            _raceCar.ApplyGas(false);
            _raceCar.ShiftUp();
            _nextGearShift = Random.Range(_opponentInformation._minGearShift, _opponentInformation._maxGearShift);
            Invoke("ApplyGas", _opponentInformation._shiftSpeed);
        }
    }

    private void ApplyGas()
    {
        _raceCar.ApplyGas(true);
    }

    private void SetOpponentStats()
    {
        _opponentInformation = LevelSelectInformation._levelSelectInformationInstance.GetLevelSelected();
    }
}
