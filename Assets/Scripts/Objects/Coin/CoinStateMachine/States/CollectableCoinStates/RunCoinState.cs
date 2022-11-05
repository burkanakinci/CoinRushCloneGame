using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCoinState : IState
{
    private CollectableCoin m_Coin;
    public RunCoinState(CollectableCoin _coin)
    {
        m_Coin = _coin;
    }

    public void Enter()
    {
    }
    public void LogicalUpdate()
    {
        m_Coin.RotateCoinVisual();
        m_Coin.RotateCoinToFrontCoin();
        m_Coin.MoveCoinToFrontCoin();
    }
    public void PhysicalUpdate()
    {
    }
    public void Exit()
    {

    }
}
