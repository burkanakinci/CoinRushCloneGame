using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCoinState : IState
{
    private Coin m_Coin;
    public RunCoinState(Coin _coin)
    {
        m_Coin = _coin;
    }

    public void Enter()
    {
    }
    public void LogicalUpdate()
    {
        m_Coin.RotateToFrontCoin();
    }
    public void PhysicalUpdate()
    {

        m_Coin.SetVelocity();
    }
    public void Exit()
    {

    }
}
