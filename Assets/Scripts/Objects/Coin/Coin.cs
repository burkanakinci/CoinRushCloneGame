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

    [HideInInspector] public Coin FrontCoin;
    [HideInInspector] public Coin BackCoin;
    #endregion

    #region ExternalAccess
    public Transform BackCoinParent => m_BackCoinParent;
    #endregion

    public override void Initialize()
    {
        m_VisualPunchSequenceID = GetInstanceID() + "m_VisualPunchTweenID";
        m_EmplacementFinishAreaTweenID = GetInstanceID() + "m_EmplacementFinishAreaTweenID";
        m_EmplacementFinishStairTweenID = GetInstanceID() + "m_EmplacementFinishStairTweenID";
    }

    public virtual void KillAllTween()
    {
        DOTween.Kill(m_VisualPunchSequenceID);
        DOTween.Kill(m_EmplacementFinishAreaTweenID);
        DOTween.Kill(m_EmplacementFinishStairTweenID);
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

    private string m_EmplacementFinishAreaTweenID;
    private Sequence m_EmplacementFinishAreaSequence;
    private Quaternion m_TempEmplacementFinishRot;
    public void StartFinishPlacementSequence(Vector3 _placementPos)
    {

        DOTween.Kill(m_EmplacementFinishAreaTweenID);

        m_TempEmplacementFinishRot = Quaternion.Euler(0.0f, 90.0f, 0.0f);

        m_EmplacementFinishAreaSequence = DOTween.Sequence().SetId(m_EmplacementFinishAreaTweenID);
        m_EmplacementFinishAreaSequence.Append(
            transform.DOMove(_placementPos, 1f).SetEase(Ease.Linear)
        );
        m_EmplacementFinishAreaSequence.InsertCallback(0.5f,
        () =>
        {
            transform.DORotateQuaternion(m_TempEmplacementFinishRot, 0.5f).SetEase(Ease.Linear);
            if (BackCoin != null)
            {
                BackCoin.StartFinishPlacementSequence((_placementPos + (Vector3.up * 1.5f)));
            }
            else
            {
                GameManager.Instance.PlayerManager.MainCoin.StartFinishStairPlacementSequence();
            }
        });
    }

    private string m_EmplacementFinishStairTweenID;
    private Sequence m_EmplacementFinishStairSequence;
    public void StartFinishStairPlacementSequence()
    {

        DOTween.Kill(m_EmplacementFinishStairTweenID);

        m_TempEmplacementFinishRot = Quaternion.Euler(0.0f, 90.0f, 0.0f);

        m_EmplacementFinishStairSequence = DOTween.Sequence().SetId(m_EmplacementFinishStairTweenID);
        m_EmplacementFinishStairSequence.Append(
            transform.DOMove(GameManager.Instance.Finish.GetEmptyStairPos(), 0.75f).SetEase(Ease.Linear)
        );
        m_EmplacementFinishStairSequence.InsertCallback(0.25f,
        () =>
        {
            if (BackCoin != null)
            {
                BackCoin.StartFinishStairPlacementSequence();
            }
        });
    }
}
