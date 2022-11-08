using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStateMachine : StateMachine
{
    public CoinStateMachine()
    {
        m_States = new List<IState>();


    }

    public void ChangeCoinState(int _stateIndex, bool _changeForce = false)
    {
        ChangeState(_stateIndex, _changeForce);
    }

    public bool EqualState(int _stateIndex)
    {
        return EqualCurrentState(_stateIndex);
    }
}
