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

        m_EmplacementTweenID = GetInstanceID() + "m_EmplacementTweenID";

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += OnLevelFailed;
    }

    public override void KillAllTween()
    {
        base.KillAllTween();
        DOTween.Kill(m_EmplacementTweenID);
    }

    public override void CollectedObstacle()
    {
        m_CoinStateMachine.ChangeCoinState((int)MainCoinStates.IdleCoinState, true);
        GameManager.Instance.LevelFailed();
    }

    private float m_SwipeValue;
    private Vector3 m_CoinLookPos;
    private Quaternion m_CoinRotation, m_TempRotation;
    public void UpdateSwipeValue(float _swipeValue)
    {
        m_SwipeValue = _swipeValue;

        m_CoinLookPos = transform.forward;
        m_CoinLookPos.x += m_SwipeValue;
        RotateCoin();
    }

    public void RotateCoin()
    {
        if (m_IsSeated)
        {
            m_TempRotation = Quaternion.LookRotation(m_CoinLookPos);

            m_CoinRotation = Quaternion.Lerp(transform.rotation, m_TempRotation, m_MainCoinRotationMultiplier * Time.deltaTime);

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

    public void ChangeRigidbodyKinematic(bool _isKinematic)
    {
        if (_isKinematic == true)
        {
            m_CoinRB.velocity = Vector3.zero;
            m_CoinRB.isKinematic = true;
        }
        else
        {
            m_CoinRB.isKinematic = false;
        }

    }

    private Coin m_TempLastCoin;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.FallTrigger))
        {
            GameManager.Instance.LevelFailed();
        }
        else if (other.CompareTag(ObjectTags.HillTrigger))
        {
            other.gameObject.layer = (int)ObjectsLayer.Default;
            m_IsSeated = false;
            m_ActiveHill = other.GetComponent<HillTrigger>();
            StartEmplacementOnRoadTween();
        }
        else if (other.CompareTag(ObjectTags.Obstacle))
        {
            if (GameManager.Instance.PlayerManager.LastCoin == this)
            {
                GameManager.Instance.LevelFailed();
            }
            else if (other.CompareTag(ObjectTags.Obstacle))
            {
                m_TempLastCoin = GameManager.Instance.PlayerManager.LastCoin;
                GameManager.Instance.PlayerManager.LastCoin = m_TempLastCoin.FrontCoin;
                m_TempLastCoin.CollectedObstacle();
            }
        }
        else if (other.CompareTag(ObjectTags.Finish))
        {
            other.gameObject.layer = (int)ObjectsLayer.Default;
            GameManager.Instance.LevelCompleted();
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
            transform.DORotateQuaternion(m_TempEmplacementRot, 0.25f).SetEase(Ease.Linear)
        );
        m_EmplacementSequence.Join(
            transform.DOMove(m_TempEmplacementPos, 0.25f).SetEase(Ease.Linear)
        );
        m_EmplacementSequence.OnComplete(() =>
        {
            transform.rotation = m_TempEmplacementRot;
            StartChangeSeatedTrue();
        });
    }

    private Coroutine m_ChangeSeatedCoroutine;
    private void StartChangeSeatedTrue()
    {
        if (m_ChangeSeatedCoroutine != null)
        {
            StopCoroutine(m_ChangeSeatedCoroutine);
        }

        m_ChangeSeatedCoroutine = StartCoroutine(ChangeSeatedCoroutine());
    }

    private IEnumerator ChangeSeatedCoroutine()
    {
        yield return new WaitForEndOfFrame();

        m_IsSeated = true;
    }

    public bool EqualMainCoinState(MainCoinStates _state)
    {
        return m_CoinStateMachine.EqualState((int)_state);
    }

    #region Events
    private void OnResetToMainMenu()
    {
        BackCoin = null;
        FrontCoin = null;
        KillAllTween();
        transform.rotation = Quaternion.identity;
        transform.position = GameManager.Instance.LevelManager.FirtRoadPosition;
        CompletedLastSequence = false;
        StartChangeSeatedTrue();
        ChangeRigidbodyKinematic(false);
        m_CoinStateMachine.ChangeCoinState((int)MainCoinStates.RunCoinState, true);
    }
    private void OnLevelCompleted()
    {
        m_CoinStateMachine.ChangeCoinState((int)MainCoinStates.WinMainCoinState);
    }

    private void OnLevelFailed()
    {
        m_CoinStateMachine.ChangeCoinState((int)MainCoinStates.FallMainCoinState);
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
        GameManager.Instance.OnLevelCompleted -= OnLevelCompleted;
        GameManager.Instance.OnLevelFailed -= OnLevelFailed;
    }
    #endregion
}
