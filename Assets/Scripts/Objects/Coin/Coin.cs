using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : CustomBehaviour
{
    #region Attributes
    [SerializeField] private Transform m_CoinVisualParent;
    [SerializeField] private Rigidbody m_CoinRB;
    [SerializeField] private Collider m_CoinCollider;
    [SerializeField] private float m_RotationMultiplier = 1.0f, m_ForwardVelocityMultiplier = 5f;
    private CoinStateMachine m_CoinStateMachine;
    [SerializeField] private Transform m_BackCoinParent;

    public Coin FrontCoin;
    public Coin BackCoin;
    #endregion

    #region ExternalAccess
    [HideInInspector] public Transform BackCoinParent => m_BackCoinParent;
    #endregion
    public override void Initialize()
    {
        m_CoinStateMachine = new CoinStateMachine(this);
        m_CoinStateMachine.ChangeCoinState(CoinStates.RunCoinState);

        GameManager.Instance.InputManager.OnSwiped += UpdateSwipeValue;
    }

    public void Initialize2()
    {
        m_CoinRB.isKinematic = true;
        m_CoinCollider.isTrigger = true;
        m_CoinStateMachine = new CoinStateMachine(this);
        m_CoinStateMachine.ChangeCoinState(CoinStates.IdleCoinState);

        m_FrontCoinMovementTweenID = GetInstanceID() + "m_FrontCoinMovementTweenID";
    }

    private void FixedUpdate()
    {
        m_CoinStateMachine.PhysicalUpdate();
    }
    private void Update()
    {
        m_CoinStateMachine.LogicalUpdate();
    }

    private float m_SwipeValue;
    private Vector3 m_CoinLookPos;
    private Quaternion m_CoinRotation, m_TempRotation;
    public void UpdateSwipeValue(float _swipeValue)
    {
        m_SwipeValue = _swipeValue;

        m_CoinLookPos = transform.forward;
        m_CoinLookPos.x += m_SwipeValue * m_RotationMultiplier;

        RotateCoin(m_CoinLookPos);
    }
    public void RotateCoin(Vector3 _lookPos)
    {
        m_TempRotation = Quaternion.LookRotation(_lookPos);
        m_CoinRotation = Quaternion.Slerp(transform.rotation, m_TempRotation, 0.35f);

        transform.rotation = m_CoinRotation;
    }

    public void RotateToFrontCoin()
    {
        if (FrontCoin != null)
        {
            BackCoinParent.LookAt(FrontCoin.BackCoinParent);
            transform.rotation = Quaternion.Slerp(transform.rotation, BackCoinParent.rotation, 0.65f);
        }
    }

    public void SetVelocity()
    {
        m_CoinRB.velocity = transform.forward * m_ForwardVelocityMultiplier;
    }


    private void OnTriggerEnter(Collider other)
    {
        if ((m_CoinStateMachine.EqualState(CoinStates.IdleCoinState)) &&
        other.CompareTag(ObjectTags.CollectedCoin))
        {
            gameObject.layer = (int)ObjectsLayer.CoinCollected;
            FrontCoin = GameManager.Instance.PlayerManager.LastCoin;
            GameManager.Instance.PlayerManager.LastCoin = this;

            m_CoinStateMachine.ChangeCoinState(CoinStates.ParticipationPlayerCoinState);
        }
    }

    private string m_FrontCoinMovementTweenID;
    private float m_FrontCoinMovementLerpValue;
    private Vector3 m_FrontCoinMovementStartPos;
    private Quaternion m_FrontCoinMovementStartRot;
    public void MoveToFrontCoin()
    {
        m_FrontCoinMovementLerpValue = 0.0f;
        m_FrontCoinMovementStartPos = transform.position;
        m_FrontCoinMovementStartRot = transform.rotation;

        DOTween.Kill(m_FrontCoinMovementTweenID);

        DOTween.To(() => m_FrontCoinMovementLerpValue, x => m_FrontCoinMovementLerpValue = x, 1.0f, 1f).
                OnUpdate(() =>
                {
                    transform.position = Vector3.Lerp(m_FrontCoinMovementStartPos, FrontCoin.BackCoinParent.position, m_FrontCoinMovementLerpValue);
                    transform.rotation = Quaternion.Slerp(m_FrontCoinMovementStartRot, FrontCoin.transform.rotation, m_FrontCoinMovementLerpValue);
                }).
                OnComplete(() =>
                {
                    m_CoinRB.isKinematic = false;
                    m_CoinCollider.isTrigger = false;
                    FrontCoin.BackCoin = this;
                    m_CoinStateMachine.ChangeCoinState(CoinStates.RunCoinState);
                }).
                SetEase(Ease.Linear).
                SetId(m_FrontCoinMovementTweenID);
    }
}
