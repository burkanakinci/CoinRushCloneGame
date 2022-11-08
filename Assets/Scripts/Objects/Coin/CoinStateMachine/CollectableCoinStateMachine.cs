using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoinStateMachine : CoinStateMachine
{
    public CollectableCoinStateMachine(CollectableCoin _coin) : base()
    {
        m_States.Add(new IdleCoinState(_coin));
        m_States.Add(new ParticipationPlayerCoinState(_coin));
        m_States.Add(new RunCoinState(_coin));
        m_States.Add(new WinCoinState(_coin));
    }
}
