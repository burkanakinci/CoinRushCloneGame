using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : CustomBehaviour
{
    #region Attributes
    [SerializeField] protected Transform m_CoinVisualParent;
    [SerializeField] protected Transform m_CoinVisual;
    [SerializeField] protected Collider m_CoinCollider;
    [SerializeField] protected Transform m_BackCoinParent;
    protected CoinStateMachine m_CoinStateMachine;

    public Coin FrontCoin;
    public Coin BackCoin;
    #endregion

    #region ExternalAccess
    public Transform BackCoinParent => m_BackCoinParent;
    #endregion

    public override void Initialize()
    {
        m_VisualPunchSequenceID = GetInstanceID() + "m_VisualPunchTweenID";
    }

    private void FixedUpdate()
    {
        m_CoinStateMachine.PhysicalUpdate();
    }
    private void Update()
    {
        m_CoinStateMachine.LogicalUpdate();
    }

    private Vector3 m_TargetVisualAngle;
    public void RotateCoinVisual()
    {
        m_TargetVisualAngle = m_CoinVisual.localEulerAngles;
        m_TargetVisualAngle.z += 10f;
        m_CoinVisual.localEulerAngles = Vector3.Lerp(m_CoinVisual.localEulerAngles, m_TargetVisualAngle, 30.0f * Time.deltaTime);
    }

    private string m_VisualPunchSequenceID;
    private Sequence m_VisualPunchSequence;
    public void VisualPunchTween()
    {
        DOTween.Kill(m_VisualPunchSequenceID);

        m_VisualPunchSequence = DOTween.Sequence().SetId(m_VisualPunchSequenceID);

        m_VisualPunchSequence.Append(
            m_CoinVisualParent.DOScale((Vector3.one * 1.25f), 0.25f)
        );
        m_VisualPunchSequence.Append(
            m_CoinVisualParent.DOScale((Vector3.one), 0.25f)
        );
        m_VisualPunchSequence.InsertCallback(0.25f, () =>
        {
            if (BackCoin != null)
            {
                BackCoin.VisualPunchTween();
            }
        }
        );
    }
}
