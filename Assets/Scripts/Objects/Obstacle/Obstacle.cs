using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : CustomBehaviour
{
    [SerializeField] protected Transform m_TweenedTransform;
    [SerializeField] protected ObstacleType m_ObstacleType;
    [HideInInspector] public ObstacleType ObstacleType => m_ObstacleType;
    public override void Initialize()
    {
        m_ObstacleTweenID = GetInstanceID() + "m_ObstacleTween";
    }

    protected string m_ObstacleTweenID;
    public abstract void ObstacleTween();
}
