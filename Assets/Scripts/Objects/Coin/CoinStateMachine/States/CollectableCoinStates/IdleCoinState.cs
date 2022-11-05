using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCoinState : IState
{
    private CollectableCoin m_Coin;
    public IdleCoinState(CollectableCoin _coin)
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
