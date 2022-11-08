using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : CustomBehaviour, IPooledObject
{
    #region Fields
    [SerializeField] private Transform m_LastPosition;
    [SerializeField] private HillTrigger m_HillTrigger;
    #endregion
    #region ExternalAccess
    public Transform LastPosition => m_LastPosition;
    #endregion

    public override void Initialize()
    {

    }

    public void OnObjectSpawn()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject += OnObjectDeactive;

        m_HillTrigger.gameObject.layer = (int)ObjectsLayer.HillTrigger;
    }
    public void OnObjectDeactive()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject -= OnObjectDeactive;

        GameManager.Instance.ObjectPool.AddObjectPool(PooledObjectTags.Road, this);
        this.gameObject.SetActive(false);
    }
    public CustomBehaviour GetGameObject()
    {
        return this;
    }
}
