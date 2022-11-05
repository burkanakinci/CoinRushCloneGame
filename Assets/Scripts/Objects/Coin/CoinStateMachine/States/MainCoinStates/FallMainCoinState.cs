using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallMainCoinState : IState
{
    private MainCoin m_Coin;
    public FallMainCoinState(MainCoin _coin)
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
        m_Coin.SetFallVelocity();
    }
    public void Exit()
    {

    }
}
