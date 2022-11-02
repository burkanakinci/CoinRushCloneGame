using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : CustomBehaviour
{
    #region Attributes
    [SerializeField] private Transform m_CoinVisualParent;
    [SerializeField] private Rigidbody m_CoinRB;
    private float m_SwipeValue;
    [SerializeField] private float m_HorizontalVelocityMultiplier = 1.0f, m_ForwardVelocityMultiplier = 5f;
    #endregion
    public override void Initialize()
    {
        GameManager.Instance.InputManager.OnSwiped += UpdateSwipeValue;
    }

    private float m_CurrentSplineFollowSpeed, m_StartSplineFollowSpeed;
    private float m_CoinYRotDiff;
    private Vector3 m_CoinLookPos;
    private Quaternion m_CoinRotation, m_TempRotation;

    private float m_TempYRot;
    private void UpdateSwipeValue(float _swipeValue)
    {
        m_SwipeValue = _swipeValue;

        m_CoinLookPos = m_CoinVisualParent.forward;
        m_CoinLookPos.x += m_SwipeValue * m_HorizontalVelocityMultiplier;

        m_TempRotation = Quaternion.LookRotation(m_CoinLookPos);
        m_CoinRotation = Quaternion.Slerp(m_CoinVisualParent.rotation, m_TempRotation, 0.5f);
        m_CoinVisualParent.rotation = m_CoinRotation;

        m_TempYRot = (m_CoinVisualParent.eulerAngles).y;
        if (((m_CoinVisualParent.eulerAngles).y) > 90.0f)
        {
            m_TempYRot = 360.0f - m_TempYRot;
        }
        m_TempYRot = Mathf.Abs(m_TempYRot);
        m_CoinYRotDiff = (90.0f - m_TempYRot);
        m_CurrentSplineFollowSpeed = m_CoinYRotDiff / 90.0f;
        m_ForwardVelocityMultiplier = 1.0f - m_CurrentSplineFollowSpeed;
        if (((m_CoinVisualParent.eulerAngles).y) > 90.0f)
        {
            m_ForwardVelocityMultiplier *= -1.0f;
        }

        GameManager.Instance.PlayerManager.SetSplineFollowSpeed(ref m_CurrentSplineFollowSpeed);
    }
    private void RotateCoin()
    {

    }

    private void SetCoinVelocity()
    {

    }

    private Vector3 m_TempVelocity;


    private void FixedUpdate()
    {
        m_CoinRB.velocity = transform.right * m_ForwardVelocityMultiplier;
    }
}
