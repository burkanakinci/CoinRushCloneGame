using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : CustomBehaviour
{
    #region Attributes
    [SerializeField] private Transform[] m_FinishPlacementTransforms;
    [SerializeField] private Transform m_FinishMainCoinPlacement;
    private int m_EmptyStairIndex;
    #endregion

    #region ExternalAccess
    public Transform FinishMainCoinPlacement => m_FinishMainCoinPlacement;
    private int EmptyStairIndex => m_EmptyStairIndex;
    #endregion

    public override void Initialize()
    {
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
    }

    private void SetFinishPosition()
    {
        transform.position = GameManager.Instance.LevelManager.LastRoadPosition;
    }

    private Vector3 m_TempEmptyStairIndex;
    public Vector3 GetEmptyStairPos()
    {
        m_TempEmptyStairIndex = m_FinishPlacementTransforms[m_EmptyStairIndex].position;
        m_EmptyStairIndex++;
        return m_TempEmptyStairIndex;
    }

    #region Events
    private void OnResetToMainMenu()
    {
        SetFinishPosition();
        m_EmptyStairIndex = 0;
    }
    private void OnLevelCompleted()
    {

    }

    private void OnLevelFailed()
    {
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
    }
    #endregion
}
