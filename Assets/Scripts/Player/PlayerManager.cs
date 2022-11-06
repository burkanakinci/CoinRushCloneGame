using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : CustomBehaviour
{
    #region Attributes
    public MainCoin MainCoin;
    public CollectableCoin CollectableCoin;
    public SlidingObstacle SlidingObstacle;
    public Coin LastCoin;
    #endregion
    #region ExternalAccess

    #endregion
    public override void Initialize()
    {
        MainCoin.Initialize();
        LastCoin = MainCoin;
        CollectableCoin.Initialize();
        SlidingObstacle.Initialize();
    }
}
