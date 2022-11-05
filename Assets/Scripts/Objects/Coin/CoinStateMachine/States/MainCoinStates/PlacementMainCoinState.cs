using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementMainCoinState : IState
{
    private MainCoin m_Coin;
    public PlacementMainCoinState(MainCoin _coin)
    {
        m_Coin = _coin;
    }

    public void Enter()
    {
    }
    public void LogicalUpdate()
    {
    }
    public void PhysicalUpdate()
    {
    }
    public void Exit()
    {
    }
}
