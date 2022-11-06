using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableCoin : Coin, IPooledObject
{
    #region MovementLerpMultiplier
    [SerializeField] private float m_RotateFrontCoinLerpLerp = 50.0f, m_MoveFrontCoinLerp = 50.0f;
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

        gameObject.layer = (int)ObjectsLayer.CoinCollectable;
        m_CoinCollider.isTrigger = true;
        m_CoinStateMachine.ChangeCoinState(CollectableCoinStates.IdleCoinState, true);
    }
    public void OnObjectDeactive()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject -= OnObjectDeactive;

        GameManager.Instance.ObjectPool.AddObjectPool(PooledObjectTags.CollectableCoin, this);
        this.gameObject.SetActive(false);
    }
    public CustomBehaviour GetGameObject()
    {
        return this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.CollectedCoin))
        {
            gameObject.layer = (int)ObjectsLayer.CoinCollected;
            FrontCoin = GameManager.Instance.PlayerManager.LastCoin;
            FrontCoin.BackCoin = this;
            transform.SetParent(FrontCoin.BackCoinParent);
            GameManager.Instance.PlayerManager.LastCoin = this;
            MoveToFrontCoinSequence();
            m_CoinStateMachine.ChangeCoinState(CollectableCoinStates.ParticipationPlayerCoinState);
        }
        else if (other.CompareTag(ObjectTags.Obstacle))
        {

            // coin faille

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
            GameManager.Instance.PlayerManager.MainCoin.VisualPunchTween();
            m_CoinStateMachine.ChangeCoinState(CollectableCoinStates.RunCoinState);
        });
        m_FrontCoinMovementSequence.Join(
            transform.DOLocalRotateQuaternion(Quaternion.identity, 1f)
        );
    }

    private Quaternion m_TargetRotation;
    public void RotateCoinToFrontCoin()
    {
        m_TargetRotation = FrontCoin.transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, m_TargetRotation, m_RotateFrontCoinLerpLerp * Time.deltaTime);
    }

    private Vector3 m_TargetPosition;
    public void MoveCoinToFrontCoin()
    {
        m_TargetPosition = FrontCoin.BackCoinParent.transform.position;
        transform.position = Vector3.Lerp(transform.position, m_TargetPosition, m_MoveFrontCoinLerp * Time.deltaTime);
    }
}
