using System;
using UnityEngine;
using DG.Tweening;
public class CameraManager : CustomBehaviour
{
    #region Attributes
    [SerializeField] private Camera m_MainCamera;
    [SerializeField] private CameraMovementData m_CameraMovementData;
    private Transform m_FollowedObject;
    [SerializeField] private Transform m_PLayFollowedObject;
    [SerializeField] private Transform m_FinishFollowedObject;
    #endregion


    private Vector3 lookPos;
    private Quaternion camRotation;
    public override void Initialize()
    {
        m_FinishCameraSequenceTweenID = GetInstanceID() + "m_FinishCameraSequenceTweenID";

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += OnLevelFailed;
    }

    public void LateUpdate()
    {
        if (m_FollowedObject != m_PLayFollowedObject)
        {
            return;
        }
        FollowCamera();
        LookFollowedObject();
    }

    private Vector3 m_LookPos;
    private Quaternion m_TargetRotation;
    private void LookFollowedObject()
    {

        lookPos = m_FollowedObject.position - m_MainCamera.transform.position;

        m_TargetRotation = Quaternion.LookRotation(lookPos);
        m_MainCamera.transform.rotation = m_TargetRotation;

    }
    private void FollowCamera()
    {
        m_MainCamera.transform.position = m_FollowedObject.position + m_CameraMovementData.PlayPositionOffset;
    }

    private void RotateStartCamera()
    {
        m_MainCamera.transform.rotation = m_CameraMovementData.PlayRotation;
    }

    private string m_FinishCameraSequenceTweenID;
    private Sequence m_FinishCameraSequence;
    private void StartFinishCameraSequence()
    {
        DOTween.Kill(m_FinishCameraSequenceTweenID);

        m_FinishCameraSequence = DOTween.Sequence().SetId(m_FinishCameraSequenceTweenID);

        m_FinishCameraSequence.Append(
            m_MainCamera.transform.DOMove((m_FollowedObject.position + m_CameraMovementData.FinishPositionOffset), 0.75f)
        );
        m_FinishCameraSequence.Join(
            m_MainCamera.transform.DORotateQuaternion(m_CameraMovementData.FinishRotation, 0.75f)
        );
    }

    private void KillAllTween()
    {
        DOTween.Kill(m_FinishCameraSequenceTweenID);
    }
    #region Events

    private void OnResetToMainMenu()
    {
        m_FollowedObject = m_PLayFollowedObject;
        RotateStartCamera();
        KillAllTween();
    }
    private void OnLevelCompleted()
    {
        m_FollowedObject = m_FinishFollowedObject;
        StartFinishCameraSequence();
    }

    private void OnLevelFailed()
    {

    }

    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
        GameManager.Instance.OnLevelCompleted -= OnLevelCompleted;
        GameManager.Instance.OnLevelFailed -= OnLevelFailed;
    }

    #endregion
}
