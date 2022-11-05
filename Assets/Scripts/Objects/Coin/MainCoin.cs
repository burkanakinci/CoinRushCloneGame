using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCoin : Coin
{
    [SerializeField] private float m_MainCoinRotationMultiplier = 1.0f, m_MainCoinForwardMultiplier = 5f;
    [SerializeField] private Rigidbody m_CoinRB;
    public override void Initialize()
    {
        base.Initialize();

        m_CoinStateMachine = new MainCoinStateMachine(this);
        m_CoinStateMachine.ChangeCoinState(MainCoinStates.RunCoinState,true);

        GameManager.Instance.InputManager.OnSwiped += UpdateSwipeValue;
    }

    private float m_SwipeValue;
    private Vector3 m_CoinLookPos;
    private Quaternion m_CoinRotation, m_TempRotation;
    public void UpdateSwipeValue(float _swipeValue)
    {
        m_SwipeValue = _swipeValue;

        m_CoinLookPos = transform.forward;
        m_CoinLookPos.x += m_SwipeValue * m_MainCoinRotationMultiplier;

        RotateCoin(m_CoinLookPos);
    }

    public void RotateCoin(Vector3 _lookPos)
    {
        m_TempRotation = Quaternion.LookRotation(_lookPos);
        m_CoinRotation = Quaternion.Lerp(transform.rotation, m_TempRotation, 50f * Time.deltaTime);

        transform.rotation = m_CoinRotation;
    }

    public void SetForwardVelocity()
    {
        m_CoinRB.velocity = transform.forward * m_MainCoinForwardMultiplier * Time.fixedDeltaTime;
    }
}
