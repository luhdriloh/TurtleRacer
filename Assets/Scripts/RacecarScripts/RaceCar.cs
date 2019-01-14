using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCar : MonoBehaviour
{
    public int _maxSpeed;
    public int _redline;
    public int _minRpm;
    public float _finalDriveRatio;
    public float _driveWheelRadius;

    public List<float> _gearRatios;
    public AnimationCurve _torqueCurve;

    private Rigidbody2D _rigidbody;
    private GaugeController _gaugeController;
    private EngineSound _engineSound;
    private bool _gasPressed;
    private int _gearIn; // -1 will be for neutral
    private float _rpm;
    private float _wheelRollingCircumference;
    private int _throttleModifier;
    private bool _changedGears;

	private void Awake ()
    {
        _gaugeController = GetComponentInChildren<GaugeController>();
        _engineSound = GetComponentInChildren<EngineSound>();
        if (_engineSound != null)
        {
            _engineSound._maxRpm = _redline;
        }

        _gearIn = 0;
        _rpm = _minRpm;
        _wheelRollingCircumference = _driveWheelRadius * 2 * Mathf.PI;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update ()
    {
        _throttleModifier = _gasPressed && _rpm < _redline ? 1 : 0;

        // get wheel rpm
        float effectiveDriveRatio = (_gearRatios[_gearIn] * _finalDriveRatio);
        _rpm = (_rigidbody.velocity.y / (_wheelRollingCircumference / 60f)) * effectiveDriveRatio;
        float torqueValue = _torqueCurve.Evaluate(_rpm);
        float force = (torqueValue * effectiveDriveRatio * _throttleModifier) / _driveWheelRadius;
        force -= 1500f;

        if (_rpm > _redline)
        {
            force -= 20000;
            Debug.Log("Went past redline");
        }

        if (_changedGears)
        {
            force -= 10000f;
            _changedGears = false;
        }
        // drag of 1000

        float acceleration = force / _rigidbody.mass;

        // update rpm
        Vector2 newVelocity = _rigidbody.velocity.y <= 0 && _gasPressed == false ? Vector2.zero : (Vector2)transform.up * acceleration * Time.deltaTime;
        _rigidbody.velocity += newVelocity;

        if (_gaugeController != null)
        {
            _gaugeController.UpdateGauges(_rigidbody.velocity.y, Mathf.Max(_minRpm, _rpm));
        }

        if (_engineSound != null)
        {
            _engineSound.UpdateRpm(_rpm);
        }
    }

    private float Slope()
    {
        float before = _torqueCurve.Evaluate(_rpm - 20);
        float after = _torqueCurve.Evaluate(_rpm + 20);

        return (after - before) / 40f;
    }

    public void ApplyGas(bool gasPressed)
    {
        _gasPressed = gasPressed;
    }

    public void ShiftUp()
    {
        if (_gasPressed == false && _gearIn < _gearRatios.Count)
        {
            _gearIn++;
            _changedGears = true;
        }
    }

    public float GetCurrentRpm()
    {
        return _rpm;
    }

    public float GetCurrentSpeed()
    {
        return _rigidbody.velocity.y;
    }
}
