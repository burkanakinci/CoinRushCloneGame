using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCoin : Coin
{
    [SerializeField] private float m_MainCoinRotationMultiplier = 1.0f, m_MainCoinForwardMultiplier = 5f;
    [SerializeField] private Rigidbody m_CoinRB;
    private bool m_IsSeated;
    public override void Initialize()
    {
        base.Initialize();

        m_CoinStateMachine = new MainCoinStateMachine(this);
        m_CoinStateMachine.ChangeCoinState(MainCoinStates.RunCoinState, true);

        m_EmplacementTweenID = GetInstanceID() + "m_EmplacementTweenID";

        m_IsSeated = true;
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
        if (m_IsSeated)
        {
            m_TempRotation = Quaternion.LookRotation(_lookPos);
            m_CoinRotation = Quaternion.Lerp(transform.rotation, m_TempRotation, 50f * Time.deltaTime);

            transform.rotation = m_CoinRotation;
        }
    }

    public void SetForwardVelocity()
    {
        if (m_IsSeated)
        {
            m_CoinRB.velocity = transform.forward * m_MainCoinForwardMultiplier * Time.fixedDeltaTime;
        }
    }
    public void SetFallVelocity()
    {
        m_CoinRB.velocity = (transform.forward + Vector3.down) * (m_MainCoinForwardMultiplier * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.FallTrigger))
        {
            m_CoinStateMachine.ChangeCoinState(MainCoinStates.FallMainCoinState);
        }
        else if (other.CompareTag(ObjectTags.HillTrigger))
        {
            m_IsSeated = false;
            m_ActiveHill = other.GetComponent<HillTrigger>();
            StartEmplacementOnRoadTween();
        }
    }

    private Coroutine m_StartEmplacementCoroutine;
    private void StartEmplacementOnRoadTween()
    {
        if (m_StartEmplacementCoroutine != null)
        {
            StopCoroutine(m_StartEmplacementCoroutine);
        }

        m_StartEmplacementCoroutine = StartCoroutine(StartEmplacement());
    }

    private IEnumerator StartEmplacement()
    {
        yield return new WaitForEndOfFrame();

        EmplacementOnRoadTween();
    }
    private HillTrigger m_ActiveHill;
    private string m_EmplacementTweenID;
    private Sequence m_EmplacementSequence;
    private Vector3 m_TempEmplacementPos;
    private Quaternion m_TempEmplacementRot;
    private void EmplacementOnRoadTween()
    {
        DOTween.Kill(m_EmplacementTweenID);

        m_TempEmplacementPos = transform.position;
        m_TempEmplacementPos.y = m_ActiveHill.PlacementTransform.position.y;
        m_TempEmplacementPos.z = m_ActiveHill.PlacementTransform.position.z;

        m_TempEmplacementRot = transform.rotation;
        m_TempEmplacementRot.x = m_ActiveHill.PlacementTransform.rotation.x;
        m_TempEmplacementRot.z = m_ActiveHill.PlacementTransform.rotation.z;

        m_EmplacementSequence = DOTween.Sequence().SetId(m_EmplacementTweenID);
        m_EmplacementSequence.Append(
            transform.DORotateQuaternion(m_TempEmplacementRot, 0.4f)
        );
        m_EmplacementSequence.Append(
            transform.DOMove(m_TempEmplacementPos, 0.6f)
        ).OnComplete(() =>
        {
            m_IsSeated = true;
        });
    }
}
