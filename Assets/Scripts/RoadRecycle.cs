using UnityEngine;

public class RoadRecycle : MonoBehaviour
{
    public Transform _playerTransform;
    public GameObject _roadProtoype;
    public float _roadHeight;

    private GameObject _roadOne;
    private GameObject _roadTwo;
    private int _roadToPlace;
    private float _finishDistance;

    private void Start ()
    {
        _roadOne = Instantiate(_roadProtoype, Vector3.right * (_playerTransform.position.x - .5f), Quaternion.identity);
        _roadTwo = Instantiate(_roadProtoype, new Vector3(_playerTransform.position.x - .5f, _roadHeight, 0f), Quaternion.identity);
        _roadToPlace = 0;

        if (LevelSelectInformation._levelSelectInformationInstance != null)
        {
            _finishDistance = LevelSelectInformation._levelSelectInformationInstance.GetLevelSelected()._raceDistance;
        }
        else
        {
            _finishDistance = -1;
        }
    }

    private void Update ()
    {
        if (_finishDistance > 0 && _playerTransform.position.y >= _finishDistance)
        {
            return;
        }

        if (_roadToPlace == 0 && _roadTwo.transform.position.y < _playerTransform.position.y)
        {
            _roadOne.transform.position = _roadTwo.transform.position + new Vector3(0f, _roadHeight, 0f);
            _roadToPlace = 1;
        }
        else if (_roadToPlace == 1 && _roadOne.transform.position.y < _playerTransform.position.y)
        {
            _roadTwo.transform.position = _roadOne.transform.position + new Vector3(0f, _roadHeight, 0f);
            _roadToPlace = 0;
        }
    }
}
