using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : CustomBehaviour
{
    [SerializeField] private Transform[] m_FinishPlacementTransforms;
    public override void Initialize()
    {
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
    }

    private void SetFinishPosition()
    {
        transform.position = GameManager.Instance.LevelManager.LastRoadPosition;
    }

    #region Events
    private void OnResetToMainMenu()
    {
        SetFinishPosition();
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
