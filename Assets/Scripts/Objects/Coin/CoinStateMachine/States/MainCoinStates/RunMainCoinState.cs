using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunMainCoinState : IState
{
    private MainCoin m_Coin;
    public RunMainCoinState(MainCoin _coin)
    {
        m_Coin = _coin;
    }

    public void Enter()
    {
        GameManager.Instance.InputManager.OnSwiped += m_Coin.UpdateSwipeValue;
    }
    public void LogicalUpdate()
    {
        m_Coin.RotateCoinVisual();
    }
    public void PhysicalUpdate()
    {
        m_Coin.SetForwardVelocity();
    }
    public void Exit()
    {
        GameManager.Instance.InputManager.OnSwiped -= m_Coin.UpdateSwipeValue;
    }
}
