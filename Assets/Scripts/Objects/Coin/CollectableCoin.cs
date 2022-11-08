using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableCoin : Coin, IPooledObject
{
    #region MovementLerpMultiplier
    [SerializeField] private CoinData m_CoinData;
    #endregion
    public override void Initialize()
    {
        base.Initialize();
        m_CoinStateMachine = new CollectableCoinStateMachine(this);

        m_FrontCoinMovementSequenceID = GetInstanceID() + "m_FrontCoinMovementSequenceID";

    }
    public void OnObjectSpawn()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject += OnObjectDeactive;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;

        CompletedLastSequence = false;

        BackCoin = null;
        FrontCoin = null;

        gameObject.layer = (int)ObjectsLayer.CoinCollectable;
        gameObject.tag = ObjectTags.Untagged;
        m_CoinCollider.isTrigger = true;
        m_CoinStateMachine.ChangeCoinState((int)CollectableCoinStates.IdleCoinState, true);
    }
    public void OnObjectDeactive()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject -= OnObjectDeactive;
        GameManager.Instance.OnLevelCompleted -= OnLevelCompleted;

        KillAllTween();

        GameManager.Instance.ObjectPool.AddObjectPool(PooledObjectTags.CollectableCoin, this);
        this.gameObject.SetActive(false);
    }
    public CustomBehaviour GetGameObject()
    {
        return this;
    }

    public override void KillAllTween()
    {
        base.KillAllTween();
        DOTween.Kill(m_FrontCoinMovementSequenceID);
    }

    public override void CollectedObstacle()
    {
        OnObjectDeactive();
    }

    private Coin m_TempLastCoin;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.CollectedCoin))
        {
            gameObject.layer = (int)ObjectsLayer.CoinCollected;
            m_TempLastCoin = GameManager.Instance.PlayerManager.LastCoin;
            GameManager.Instance.PlayerManager.LastCoin = this;
            FrontCoin = m_TempLastCoin;
            m_TempLastCoin.BackCoin = this;
            transform.SetParent(m_TempLastCoin.BackCoinParent);
            MoveToFrontCoinSequence();
            m_CoinStateMachine.ChangeCoinState((int)CollectableCoinStates.ParticipationPlayerCoinState);
        }
        else if (other.CompareTag(ObjectTags.Obstacle))
        {
            m_TempLastCoin = GameManager.Instance.PlayerManager.LastCoin;
            GameManager.Instance.PlayerManager.LastCoin = m_TempLastCoin.FrontCoin;
            m_TempLastCoin.CollectedObstacle();
        }
    }

    private string m_FrontCoinMovementSequenceID;
    private Sequence m_FrontCoinMovementSequence;
    public void MoveToFrontCoinSequence()
    {
        DOTween.Kill(m_FrontCoinMovementSequenceID);

        m_FrontCoinMovementSequence = DOTween.Sequence().SetId(m_FrontCoinMovementSequenceID);
        m_FrontCoinMovementSequence.Append(
            transform.DOLocalJump(Vector3.zero, 1f, 1, 1f)
        ).OnComplete(() =>
        {
            m_CoinCollider.isTrigger = false;
            transform.SetParent(null);
            gameObject.tag = ObjectTags.CollectedCoin;
            GameManager.Instance.PlayerManager.MainCoin.VisualPunchTween();
            m_CoinStateMachine.ChangeCoinState((int)CollectableCoinStates.RunCoinState);
        });
        m_FrontCoinMovementSequence.Join(
            transform.DOLocalRotateQuaternion(Quaternion.identity, 1f)
        );
    }

    private Quaternion m_TargetRotation;
    public void RotateCoinToFrontCoin()
    {
        m_TargetRotation = FrontCoin.transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, m_TargetRotation, m_CoinData.RotateFrontCoinLerp* Time.deltaTime);
    }

    private Vector3 m_TargetPosition;
    public void MoveCoinToFrontCoin()
    {
        m_TargetPosition = FrontCoin.BackCoinParent.transform.position;
        transform.position = Vector3.Lerp(transform.position, m_TargetPosition, m_CoinData.MoveFrontCoinLerp * Time.deltaTime);
    }

    #region Events
    private void OnLevelCompleted()
    {
        m_CoinStateMachine.ChangeCoinState((int)CollectableCoinStates.WinCoinState);
    }

    #endregion
}
