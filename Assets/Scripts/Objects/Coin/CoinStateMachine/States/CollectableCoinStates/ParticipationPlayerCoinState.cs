using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticipationPlayerCoinState : IState
{
    private CollectableCoin m_Coin;
    public ParticipationPlayerCoinState(CollectableCoin _coin)
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
        m_Coin.transform.SetParent(null);
    }
}
