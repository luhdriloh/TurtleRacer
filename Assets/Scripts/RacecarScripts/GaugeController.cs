using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeController : MonoBehaviour
{
    public GameObject _speedNeedle;
    public GameObject _rpmNeedle;
    public SpriteRenderer _rpmColor;

    public float _maxSpeed;
    public float _maxRpm;
    public float _redline;

    public float _minAngle;
    public float _maxAngle;

    public float _minRpmAngle;
    public float _maxRpmAngle;

    private float _minMaxAngleDifference;
    private float _minMaxRpmAngleDifference;

    private void Start ()
    {
        _minMaxAngleDifference = Mathf.Abs(_maxAngle - _minAngle);
        _minMaxRpmAngleDifference = Mathf.Abs(_maxRpmAngle - _minRpmAngle);
    }
	
	private void Update ()
    {
		
	}

    public void UpdateGauges(float velocity, float rpm)
    {
        velocity *= 2.2369363f;

        float percentMaxSpeed = velocity / _maxSpeed;
        float percentMaxRpm = rpm / _maxRpm;
        float targetRpmRotation = (_minRpmAngle - percentMaxRpm * _minMaxRpmAngleDifference);

        _speedNeedle.transform.eulerAngles = new Vector3(0f, 0f, _minAngle - percentMaxSpeed * _minMaxAngleDifference);
        _rpmNeedle.transform.eulerAngles = new Vector3(0f, 0f, targetRpmRotation);

        if (rpm > _redline)
        {
            _rpmColor.color = Color.red;
        }
        else
        {
            _rpmColor.color = Color.white;
        }
    }
}
