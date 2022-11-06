using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurningObstacle : Obstacle
{
    public override void Initialize()
    {
        base.Initialize();
        ObstacleTween();
    }
    private float m_TweenLerpValue;
    private Vector3 m_TargetLocalEuler, m_StartLocalEuler;
    public override void ObstacleTween()
    {
        DOTween.Kill(m_ObstacleTweenID);

        m_TweenLerpValue = 0.0f;
        m_TargetLocalEuler = m_TweenedTransform.localEulerAngles;
        m_StartLocalEuler = m_TweenedTransform.localEulerAngles;
        m_TargetLocalEuler.y += 360.0f;

        DOTween.To(() => m_TweenLerpValue, x => m_TweenLerpValue = x, 1, 5.0f).
        OnUpdate(() => TurnObstacle()).
        OnComplete(() =>
        {
            ObstacleTween();
        }).
        SetEase(Ease.Linear).
        SetId(m_TweenLerpValue);
    }

    private void TurnObstacle()
    {
        m_TweenedTransform.localEulerAngles = Vector3.Lerp(m_StartLocalEuler, m_TargetLocalEuler, m_TweenLerpValue);
    }
}
