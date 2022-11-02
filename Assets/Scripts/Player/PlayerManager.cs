using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Dreamteck.Splines;

public class PlayerManager : CustomBehaviour
{
    #region Attributes
    [SerializeField] private Coin m_MainCoin;
    [SerializeField] private SplineFollower m_PlayerSplineFollower;

    #endregion
    #region ExternalAccess

    #endregion
    public override void Initialize()
    {
        m_StartSplineFollowSpeed = m_PlayerSplineFollower.followSpeed;
        m_MainCoin.Initialize();

    }

    private float m_StartSplineFollowSpeed;
    public void SetSplineFollowSpeed(ref float _currentFollowSpeed)
    {
        m_PlayerSplineFollower.followSpeed = m_StartSplineFollowSpeed * _currentFollowSpeed;
    }
}
