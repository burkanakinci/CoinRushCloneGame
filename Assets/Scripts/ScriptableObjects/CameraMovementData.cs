using UnityEngine;

[CreateAssetMenu(fileName = "CameraMovementData", menuName = "Camera Movement Data")]
public class CameraMovementData : ScriptableObject
{
    #region Attributes
    [SerializeField] private Vector3 m_PlayPositionOffset;
    [SerializeField] private Quaternion m_StartRotation;

    [SerializeField] private Vector3 m_FinishPositionOffset;
    [SerializeField] private Quaternion m_FinishRotation;
    #endregion

    #region ExternalAccess
    public Vector3 PlayPositionOffset => m_PlayPositionOffset;
    public Quaternion PlayRotation => m_StartRotation;

    public Vector3 FinishPositionOffset => m_FinishPositionOffset;
    public Quaternion FinishRotation => m_FinishRotation;
    #endregion
}
