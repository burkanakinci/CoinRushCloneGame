using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticipationPlayerCoinState : IState
{
    private Coin m_Coin;
    public ParticipationPlayerCoinState(Coin _coin)
    {
        m_Coin = _coin;
    }

    public void Enter()
    {
        m_Coin.MoveToFrontCoin();
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
