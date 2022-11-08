using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooledObjects : CustomBehaviour, IPooledObject
{
    [SerializeField] private string m_PooledTag;
    [SerializeField] private ParticleSystem m_PlayedParticle;

    public override void Initialize()
    {

    }
    public void OnObjectSpawn()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject += OnObjectDeactive;
        m_PlayedParticle.Play();
        StartParticleIsAliveCoroutine();
    }
    public void OnObjectDeactive()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject -= OnObjectDeactive;
        GameManager.Instance.ObjectPool.AddObjectPool(m_PooledTag, this);
        this.gameObject.SetActive(false);
    }
    public CustomBehaviour GetGameObject()
    {
        return this;
    }

    private Coroutine m_StartParticleIsAliveCoroutine;
    private void StartParticleIsAliveCoroutine()
    {
        if (m_StartParticleIsAliveCoroutine != null)
        {
            StopCoroutine(m_StartParticleIsAliveCoroutine);
        }

        m_StartParticleIsAliveCoroutine = StartCoroutine(ParticleIsAlive());
    }
    private IEnumerator ParticleIsAlive()
    {
        yield return new WaitUntil(() => (!m_PlayedParticle.IsAlive()));
        OnObjectDeactive();
    }
}
