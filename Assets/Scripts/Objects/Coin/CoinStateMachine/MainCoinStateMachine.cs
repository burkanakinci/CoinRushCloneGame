using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCoinStateMachine : CoinStateMachine
{
    public MainCoinStateMachine(MainCoin _coin) : base()
    {
        m_States.Add(new IdleMainCoinState(_coin));
        m_States.Add(new RunMainCoinState(_coin));
        m_States.Add(new FallMainCoinState(_coin));
    }
}
