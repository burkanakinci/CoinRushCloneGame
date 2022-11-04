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

    public void ChangeCoinState(CoinStates _state, bool _changeForce = false)
    {
        ChangeState((int)_state, _changeForce);
    }

    public bool EqualState(CoinStates _state)
    {
        return EqualCurrentState((int)_state);
    }
}
