using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : CustomBehaviour
{
    #region Attributes
    [SerializeField] private Transform m_CoinVisualParent;
    [SerializeField] private Rigidbody m_CoinRB;
    [SerializeField] private float m_RotationMultiplier = 1.0f, m_ForwardVelocityMultiplier = 5f;
    private CoinStateMachine m_CoinStateMachine;
    #endregion

    #region ExternalAccess

    #endregion
    public override void Initialize()
    {
        m_CoinStateMachine = new CoinStateMachine(this);
        m_CoinStateMachine.ChangeState(CoinStates.RunCoinState);

        GameManager.Instance.InputManager.OnSwiped += UpdateSwipeValue;

    }

    private void FixedUpdate()
    {
        m_CoinStateMachine.PhysicalUpdate();
    }

    private float m_SwipeValue;
    private float m_CurrentSplineFollowSpeed;
    private float m_CoinYRotDiff;
    private Vector3 m_CoinLookPos;
    private Quaternion m_CoinRotation, m_TempRotation;
    private float m_TempYRot;
    private void UpdateSwipeValue(float _swipeValue)
    {
        m_SwipeValue = _swipeValue;

        RotateCoin();
    }
    private void RotateCoin()
    {
        m_CoinLookPos = m_CoinVisualParent.forward;
        m_CoinLookPos.x += m_SwipeValue * m_RotationMultiplier;

        m_TempRotation = Quaternion.LookRotation(m_CoinLookPos);
        m_CoinRotation = Quaternion.Slerp(m_CoinVisualParent.rotation, m_TempRotation, 0.5f);
        m_CoinRB.MoveRotation(m_CoinRotation);
    }

    public void SetVelocity()
    {
        m_CoinRB.velocity = transform.forward * m_ForwardVelocityMultiplier;
    }
}
