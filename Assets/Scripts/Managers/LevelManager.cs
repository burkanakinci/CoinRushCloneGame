
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : CustomBehaviour
{
    #region Attributes
    private LevelData m_LevelData;
    private int m_CurrentLevelNumber;
    private int m_ActiveLevelDataNumber;
    private int m_MaxLevelDataCount;
    #endregion

    #region ExternalAccess
    public Vector3 LastRoadPosition => m_LevelData.RoadPositions[m_LevelData.RoadPositions.Length - 1];
    public Vector3 FirtRoadPosition => m_LevelData.RoadPositions[0];
    #endregion

    #region Actions
    public event Action OnCleanSceneObject;
    #endregion

    public override void Initialize()
    {

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += OnLevelFailed;

        m_MaxLevelDataCount = Resources.LoadAll("LevelDatas", typeof(LevelData)).Length;
    }

    private void GetLevelData()
    {
        m_ActiveLevelDataNumber = (m_CurrentLevelNumber <= m_MaxLevelDataCount) ? (m_CurrentLevelNumber) : ((int)(UnityEngine.Random.Range(1, (m_MaxLevelDataCount + 1))));
        m_LevelData = Resources.Load<LevelData>("LevelDatas/" + m_ActiveLevelDataNumber + "MainLevelData");
    }

    private void SpawnSceneObjects()
    {
        SpawnRoad();
        SpawnObstacle();
        SpawnCollectableCoin();
    }

    #region SpawnSceneObject
    private void SpawnRoad()
    {
        for (int _roadCount = 0; _roadCount < m_LevelData.RoadPositions.Length; _roadCount++)
        {
            GameManager.Instance.ObjectPool.SpawnFromPool(PooledObjectTags.Road,
                m_LevelData.RoadPositions[_roadCount],
                m_LevelData.RoadRotations[_roadCount],
                null);
        }
    }

    private string m_TempSpawnedObstacleTag;
    private void SpawnObstacle()
    {
        for (int _obstacleCount = 0; _obstacleCount < m_LevelData.ObstacleTypes.Length; _obstacleCount++)
        {
            switch (m_LevelData.ObstacleTypes[_obstacleCount])
            {
                case (ObstacleType.AxeObstacle):
                    m_TempSpawnedObstacleTag = PooledObjectTags.AxeObstacle;
                    break;
                case (ObstacleType.SlidingObstacle):
                    m_TempSpawnedObstacleTag = PooledObjectTags.SlidingObstacle;
                    break;
                case (ObstacleType.TurningObstacle):
                    m_TempSpawnedObstacleTag = PooledObjectTags.TurningObstacle;
                    break;
            }
            GameManager.Instance.ObjectPool.SpawnFromPool(m_TempSpawnedObstacleTag,
                    m_LevelData.ObstaclePositions[_obstacleCount],
                    m_LevelData.ObstacleRotations[_obstacleCount],
                    null);
        }
    }
    private void SpawnCollectableCoin()
    {
        for (int _collactableCount = 0; _collactableCount < m_LevelData.CoinPositions.Length; _collactableCount++)
        {

            GameManager.Instance.ObjectPool.SpawnFromPool(PooledObjectTags.CollectableCoin,
                    m_LevelData.CoinPositions[_collactableCount],
                    m_LevelData.CoinRotations[_collactableCount],
                    null);
        }
    }
    #endregion

    #region Events
    private void OnResetToMainMenu()
    {
        OnCleanSceneObject?.Invoke();

        m_CurrentLevelNumber = GameManager.Instance.PlayerManager.GetLevelNumber();

        GetLevelData();
        SpawnSceneObjects();
    }
    private void OnLevelCompleted()
    {

    }

    private void OnLevelFailed()
    {
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
        GameManager.Instance.OnLevelCompleted -= OnLevelCompleted;
        GameManager.Instance.OnLevelFailed -= OnLevelFailed;
    }
    #endregion
}
