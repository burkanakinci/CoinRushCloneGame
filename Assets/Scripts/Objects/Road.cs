using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : CustomBehaviour, IPooledObject
{
    #region Fields
    [SerializeField] private Transform m_LastPosition;
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
