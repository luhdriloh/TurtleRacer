using UnityEngine;

public class EngineSound : MonoBehaviour
{
    public AudioClip _idle;
    public AudioClip _high;
    public float _maxRpm;

    private AudioSource _audioSource;
    private bool _going;
    private float _currentRpm;
    private float _timeDone;
    private float _timeToSilenceEngine;

	private void Start ()
    {
        _timeToSilenceEngine = 4f;
        _audioSource = GetComponent<AudioSource>();
        _going = true;
    }

    public void UpdateRpm(float rpm)
    {
        if (GameController._gamecontroller != null && GameController._gamecontroller._raceDone)
        {
            if (_timeDone <= Mathf.Epsilon)
            {
                _timeDone = Time.time;
            }

            float timeSinceDone = Time.time - _timeDone;
            float volume = Mathf.Lerp(1f, 0f, timeSinceDone / _timeToSilenceEngine);
            _audioSource.volume = volume;
        }

        if (Mathf.Abs(rpm - _currentRpm) <= Mathf.Epsilon && _going)
        {
            _going = false;
            _audioSource.clip = _idle;
            _audioSource.Play();
            _audioSource.pitch = 1;
        }

        if (Mathf.Abs(rpm - _currentRpm) > Mathf.Epsilon)
        {
            // we are accelerating
            float percentMaxRpm = rpm / _maxRpm;

            if (_going == false)
            {
                _going = true;
                _audioSource.clip = _high;
                _audioSource.Play();
            }

            _audioSource.pitch = Mathf.Max(percentMaxRpm / .75f, .1f);
        }

        _currentRpm = rpm;
    }
}
