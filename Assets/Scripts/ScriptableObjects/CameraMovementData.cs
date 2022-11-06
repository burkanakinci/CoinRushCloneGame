using UnityEngine;

[CreateAssetMenu(fileName = "CameraMovementData", menuName = "Camera Movement Data")]
public class CameraMovementData : ScriptableObject
{
    #region Attributes
    [SerializeField] private Vector3 m_PlayPositionOffset;
    [SerializeField] private Quaternion m_PlayRotation;
    #endregion

    #region ExternalAccess
    public Vector3 PlayPositionOffset => m_PlayPositionOffset;
    public Quaternion PlayRotation => m_PlayRotation;
    #endregion
}
