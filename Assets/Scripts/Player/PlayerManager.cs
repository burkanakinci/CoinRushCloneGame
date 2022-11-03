using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : CustomBehaviour
{
    #region Attributes
    [SerializeField] private Coin m_MainCoin;

    #endregion
    #region ExternalAccess

    #endregion
    public override void Initialize()
    {
        m_MainCoin.Initialize();
    }
}
