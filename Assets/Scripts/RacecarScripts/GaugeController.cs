using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public GameObject _speedNeedle;
    public GameObject _rpmNeedle;
    public Text _gearInText;
    public SpriteRenderer _rpmColor;

    private int _gearIn;
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
        _gearIn = 1;
        _minMaxAngleDifference = Mathf.Abs(_maxAngle - _minAngle);
        _minMaxRpmAngleDifference = Mathf.Abs(_maxRpmAngle - _minRpmAngle);
    }

    public void UpdateGauges(float velocity, float rpm, int gearIn)
    {
        if (gearIn + 1 != _gearIn)
        {
            _gearIn = gearIn + 1;
            _gearInText.text = _gearIn.ToString();
        }

        velocity *= 2.2369363f;

        float percentMaxSpeed = velocity / _maxSpeed;
        float percentMaxRpm = Mathf.Min(rpm / _maxRpm, 1.01f);
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
