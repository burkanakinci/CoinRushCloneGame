using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStateMachine : StateMachine
{

    public CoinStateMachine(Coin _coin)
    {
        m_States = new List<IState>();

        m_States.Add(new IdleCoinState(_coin));
        m_States.Add(new ParticipationPlayerCoinState(_coin));
        m_States.Add(new RunCoinState(_coin));
    }
}
