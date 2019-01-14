using UnityEngine;

[CreateAssetMenu(fileName = "Level Data.asset", menuName = "Level/Level Data", order = 1)]
public class LevelData : ScriptableObject
{
    public string _opponentName;
    public int _difficulty;

    // data for the opponent
    public float _shiftSpeed;
    public float _minGearShift;
    public float _maxGearShift;
    public float _reactionTimeMin;
    public float _reactionTimeMax;
    public Color _opponentCarColor;
    public float _raceDistance;
}
