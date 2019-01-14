using UnityEngine;

public class LevelSelectInformation : MonoBehaviour
{
    public static LevelSelectInformation _levelSelectInformationInstance;
    private static LevelData _levelSelected;

    private void Awake()
    {
        if (_levelSelectInformationInstance == null)
        {
            DontDestroyOnLoad(this);
            _levelSelectInformationInstance = this;
            _levelSelected = null;
        }
        else if (_levelSelectInformationInstance != null)
        {
            Destroy(this);
        }
    }

    public void SetLevelInformation(LevelData levelSelected)
    {
        _levelSelected = levelSelected;
    }

    public LevelData GetLevelSelected()
    {
        return _levelSelected;
    }
}
