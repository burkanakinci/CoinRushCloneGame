using UnityEngine;
using System.Collections.Generic;

public class StateMachine
{

    protected IState m_CurrentState;
    protected List<IState> m_States;

    public void LogicalUpdate()
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.LogicalUpdate();
        }
    }
    public void PhysicalUpdate()
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.PhysicalUpdate();
        }
    }
    public void ChangeState(CoinStates _state, bool _changeForce = false)
    {
        if (m_States[(int)_state] != m_CurrentState || _changeForce)
        {
            if (m_CurrentState != null)
            {
                m_CurrentState.Exit();
            }

            m_CurrentState = m_States[(int)_state];
            m_CurrentState.Enter();
        }
    }
}
