using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{

    #region Datas
    public Vector3[] RoadPositions;
    public Quaternion[] RoadRotations;
    public ObstacleType[] ObstacleTypes;
    public Vector3[] ObstaclePositions;
    public Quaternion[] ObstacleRotations;
    public Vector3[] CoinPositions;
    public Quaternion[] CoinRotations;
    #endregion
}
