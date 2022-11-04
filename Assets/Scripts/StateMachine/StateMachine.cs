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
    protected void ChangeState(int _state, bool _changeForce = false)
    {
        if (m_States[_state] != m_CurrentState || _changeForce)
        {
            if (m_CurrentState != null)
            {
                m_CurrentState.Exit();
            }

            m_CurrentState = m_States[_state];
            m_CurrentState.Enter();
        }
    }

    protected bool EqualCurrentState(int _state)
    {
        return (m_CurrentState == m_States[_state]);
    }
}
