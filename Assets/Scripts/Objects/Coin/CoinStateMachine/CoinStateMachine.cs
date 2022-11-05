using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStateMachine : StateMachine
{
    public CoinStateMachine()
    {
        m_States = new List<IState>();


    }

    public void ChangeCoinState(CollectableCoinStates _state, bool _changeForce = false)
    {
        ChangeState((int)_state, _changeForce);
    }
    public void ChangeCoinState(MainCoinStates _state, bool _changeForce = false)
    {
        ChangeState((int)_state, _changeForce);
    }

    public bool EqualState(CollectableCoinStates _state)
    {
        return EqualCurrentState((int)_state);
    }
}
