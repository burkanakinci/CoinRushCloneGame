using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlidingObstacle : Obstacle, IPooledObject
{
    public override void Initialize()
    {
        base.Initialize();

    }

    public void OnObjectSpawn()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject += OnObjectDeactive;
        ObstacleTween();
    }
    public void OnObjectDeactive()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject -= OnObjectDeactive;

        GameManager.Instance.ObjectPool.AddObjectPool(PooledObjectTags.SlidingObstacle, this);
        this.gameObject.SetActive(false);
    }
    public CustomBehaviour GetGameObject()
    {
        return this;
    }

    private Sequence m_ObstacleSequence;
    private Vector3 m_TargetLocalPosition, m_TargetStartLocalPosition;
    public override void ObstacleTween()
    {
        DOTween.Kill(m_ObstacleTweenID);

        m_ObstacleSequence = DOTween.Sequence().SetId(m_ObstacleTweenID);

        m_TargetStartLocalPosition = m_TweenedTransform.localPosition;
        m_TargetLocalPosition = m_TweenedTransform.localPosition;
        m_TargetLocalPosition.x *= (-1.0f);
        m_ObstacleSequence.Append(
            m_TweenedTransform.DOLocalMove(m_TargetLocalPosition, 2.0f).SetEase(Ease.InExpo)
        );
        m_ObstacleSequence.Append(
            m_TweenedTransform.DOLocalMove(m_TargetStartLocalPosition, 2.0f).SetEase(Ease.InExpo).
                OnComplete(() =>
                {
                    ObstacleTween();
                })
        );
    }
}
