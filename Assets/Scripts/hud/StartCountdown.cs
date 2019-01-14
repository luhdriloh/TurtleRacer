using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour
{
    public enum CountdownStages
    {
        READY = 0,
        LIGHTS
    };

    public Text _countdown;
    public Image _orangeLightThree;
    public Image _orangeLightTwo;
    public Image _orangeLight;
    public Image _greenLight;
    public Image _redLight;
    public float _offLightAlphaValue;

    public float _numberDelay;
    public float _delayBetweenLights;

    private List<Image> _lightStages;
    private float _timeInCurrentStage;
    private int _readyCountdown;
    private int _lightCountdownIndex;
    private bool _countdownDone;
    private bool _turnOffInitiated;

    private CountdownStages _stageOn = CountdownStages.READY;

	private void Start ()
    {
        _lightStages = new List<Image>
        {
            _orangeLightThree,
            _orangeLightTwo,
            _orangeLight,
            _greenLight,
        };

        // red light is the foul light

        _lightCountdownIndex = 0;
        _timeInCurrentStage = 0f;
        _readyCountdown = 3;
        _countdownDone = false;
        _turnOffInitiated = false;
    }
	
	private void Update ()
    {
        _timeInCurrentStage += Time.deltaTime;

        if (_countdownDone == true)
        {
            if (_turnOffInitiated == false)
            {
                Invoke("TurnEverythingOff", _delayBetweenLights);
                _turnOffInitiated = true;
            }

            return;
        }

        if (_stageOn == CountdownStages.READY && _timeInCurrentStage >= _numberDelay)
        {
            _countdown.text = _readyCountdown.ToString();
            _readyCountdown--;
            _timeInCurrentStage = 0f;

            if (_readyCountdown < 0)
            {
                _stageOn = CountdownStages.LIGHTS;
            }
        }
        else if (_stageOn == CountdownStages.LIGHTS && _timeInCurrentStage >= _delayBetweenLights)
        {
            Color onColor = new Color(_lightStages[_lightCountdownIndex].color.r, _lightStages[_lightCountdownIndex].color.g, _lightStages[_lightCountdownIndex].color.b, 1);
            _lightStages[_lightCountdownIndex].color = onColor;
            _lightCountdownIndex++;
            _timeInCurrentStage = 0f;

            if (_lightCountdownIndex > _lightStages.Count -1)
            {
                _countdownDone = true;
                GameController._gamecontroller.SetRaceToStarted();
            }
        }
	}

    private void TurnEverythingOff()
    {
        gameObject.SetActive(false);
    }

    public float GetTimeLeft()
    {
        // get ready stage time if any
        float totalTimeLeft = 0f;

        if (_stageOn == CountdownStages.READY)
        {
            totalTimeLeft += (_numberDelay - _timeInCurrentStage);
            totalTimeLeft += (1 * _readyCountdown);
            totalTimeLeft += _lightStages.Count * _delayBetweenLights;
            return -totalTimeLeft;
        }
        else
        {
            totalTimeLeft += _delayBetweenLights - _timeInCurrentStage;
            totalTimeLeft += _delayBetweenLights * (_lightStages.Count - _lightCountdownIndex - 1);
            return -totalTimeLeft;
        }
    }

    public void TurnOnFalseStartLight()
    {
        Color orange = new Color(_orangeLight.color.r, _orangeLight.color.g, _orangeLight.color.b, _offLightAlphaValue);
        _orangeLight.color = orange;
        _orangeLightTwo.color = orange;
        _orangeLightThree.color = orange;

        Color green = new Color(_greenLight.color.r, _greenLight.color.g, _greenLight.color.b, _offLightAlphaValue);
        _greenLight.color = green;

        Color red = new Color(_redLight.color.r, _redLight.color.g, _redLight.color.b, 1);
        _redLight.color = red;

        _countdown.text = "foul";
    }
}
