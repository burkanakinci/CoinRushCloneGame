using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AxeObstacle : Obstacle, IPooledObject
{
    public override void Initialize()
    {
        base.Initialize();
        ObstacleTween();
    }

    public void OnObjectSpawn()
    {

    }
    public void OnObjectDeactive()
    {

    }
    public CustomBehaviour GetGameObject()
    {
        return this;
    }

    private Sequence m_ObstacleSequence;
    private Vector3 m_TargetDownLocalEuler;
    public override void ObstacleTween()
    {
        DOTween.Kill(m_ObstacleTweenID);

        m_ObstacleSequence = DOTween.Sequence().SetId(m_ObstacleTweenID);

        m_TargetDownLocalEuler = m_TweenedTransform.localEulerAngles;
        m_TargetDownLocalEuler.x = 70.0f;
        m_ObstacleSequence.Append(
            m_TweenedTransform.DOLocalRotate(m_TargetDownLocalEuler, 2.0f).SetEase(Ease.InExpo)
        );
        m_ObstacleSequence.Append(
            m_TweenedTransform.DOLocalRotate(Vector3.zero, 1.0f).SetEase(Ease.InExpo).
                OnComplete(() =>
                {
                    ObstacleTween();
                })
        );
    }
}
