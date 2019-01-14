using UnityEngine;

public class CameraFunctions : MonoBehaviour
{
    public Transform _playerTransform;
    private float _zStart;
    private Vector3 _playerOffset;
    private bool _stopPlayerFollow;
    private float _finishDistance;

	private void Start ()
    {
        _zStart = transform.position.z;
        _playerOffset = _playerTransform.position;

        _finishDistance = LevelSelectInformation._levelSelectInformationInstance.GetLevelSelected()._raceDistance;
    }
	
	private void Update ()
    {
        if (_playerTransform.position.y >= _finishDistance)
        {
            return;
        }

        Vector3 newPosition = _playerTransform.position - _playerOffset;
        newPosition.z = _zStart;

        transform.position = newPosition;
	}
}
