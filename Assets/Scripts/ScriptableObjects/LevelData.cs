using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{

    #region Datas
    public Vector3[] RoadPositions;
    public Quaternion[] RoadRotations;
    public ObstacleType[] ObstacleTypes;
    public Vector3[] ObstaclePositions;
    #endregion
}
