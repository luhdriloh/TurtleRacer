using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private RaceCar _raceCar;
    private bool _started;
    private bool _fouled;
    private float _timeSinceGasDrop;
    private bool _gasPressed;

	private void Awake()
    {
        _raceCar = GetComponentInChildren<RaceCar>();
	}
	
	private void Update ()
    {
        if (GameController._gamecontroller.HasRaceStarted() == false && Input.GetKeyDown(KeyCode.Space))
        {
            GameController._gamecontroller.FoulStart();
            _started = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space))
        {
            if (_gasPressed == false)
            {
                Debug.Log("shift speed: " + (Time.time - _timeSinceGasDrop)); 
            }

            _gasPressed = true;
            _raceCar.ApplyGas(true);

            if (_started == false)
            {
                _started = true;
                GameController._gamecontroller.ReactionTime(Time.time);
            }
        }
        else
        {
            if (_gasPressed)
            {
                _timeSinceGasDrop = Time.time;
            }
            _gasPressed = false;
            _raceCar.ApplyGas(false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _raceCar.ShiftUp();
        }
	}
}
