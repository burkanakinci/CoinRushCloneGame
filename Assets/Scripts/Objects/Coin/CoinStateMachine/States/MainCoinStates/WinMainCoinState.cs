using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMainCoinState : IState
{
    private MainCoin m_Coin;
    public WinMainCoinState(MainCoin _coin)
    {
        m_Coin = _coin;
    }

    public void Enter()
    {
        m_Coin.ChangeRigidbodyKinematic(true);
        m_Coin.StartFinishPlacementSequence(GameManager.Instance.Finish.FinishMainCoinPlacement.position);
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
