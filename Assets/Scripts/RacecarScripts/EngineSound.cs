using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour
{
    public AudioClip _idle;
    public AudioClip _low;
    public AudioClip _med;
    public AudioClip _high;
    public float _maxRpm;

    private AudioSource _audioSource;
    private AudioClip _currentClipIn;
    private float _currentRpm;
    private float _timeDone;
    private float _timeToSilenceEngine;

	private void Start ()
    {
        _timeToSilenceEngine = 4f;
        _audioSource = GetComponent<AudioSource>();
	}

    public void UpdateRpm(float rpm)
    {
        if (GameController._gamecontroller._raceDone)
        {
            if (_timeDone <= Mathf.Epsilon)
            {
                _timeDone = Time.time;
            }

            float timeSinceDone = Time.time - _timeDone;
            float volume = Mathf.Lerp(1f, 0f, timeSinceDone / _timeToSilenceEngine);
            _audioSource.volume = volume;
        }

        if (Mathf.Abs(rpm - _currentRpm) <= Mathf.Epsilon && _currentClipIn != _idle)
        {
            _currentClipIn = _idle;
            _audioSource.clip = _idle;
            _audioSource.Play();
            return;
        }

        if (Mathf.Abs(rpm - _currentRpm) > Mathf.Epsilon)
        {
            // we are accelerating
            float percentMaxRpm = rpm / _maxRpm;

            if (_currentClipIn != _high)
            {
                _currentClipIn = _high;
                _audioSource.clip = _high;
                _audioSource.Play();
            }

            _audioSource.pitch = percentMaxRpm / 1.0f;
        }

        _currentRpm = rpm;
    }
}
