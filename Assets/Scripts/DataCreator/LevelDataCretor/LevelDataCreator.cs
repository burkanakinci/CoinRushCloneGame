
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class LevelDataCreator : MonoBehaviour
{
    [HideInInspector] public string CreatedLevelName;
    [HideInInspector] public LevelData TempLevelData;
    private string savePath;
    #region Prefabs
    [SerializeField] private GameObject m_RoadPrefab;
    [SerializeField] private GameObject m_AxeObstaclePrefab;
    [SerializeField] private GameObject m_TurningObstaclePrefab;
    [SerializeField] private GameObject m_SlidingObstaclePrefab;
    [SerializeField] private GameObject m_CollectableCoinPrefab;
    #endregion

    #region Attributes
    [SerializeField] private int m_RoadCount = 7;
    #endregion

    public void SaveLevel()
    {
        TempLevelData = ScriptableObject.CreateInstance<LevelData>();

        savePath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/LevelDatas/" + CreatedLevelName + "LevelDataData.asset");

        #region SaveRoads
        m_RoadsOnScene = GameObject.FindGameObjectsWithTag(ObjectTags.Road).ToList();
        TempLevelData.RoadPositions = new Vector3[m_RoadsOnScene.Count];
        TempLevelData.RoadRotations = new Quaternion[m_RoadsOnScene.Count];
        for (int _roadCount = 0; _roadCount < m_RoadsOnScene.Count; _roadCount++)
        {
            TempLevelData.RoadPositions[_roadCount] = m_RoadsOnScene[_roadCount].transform.position;
            TempLevelData.RoadRotations[_roadCount] = m_RoadsOnScene[_roadCount].transform.rotation;
        }
        #endregion

        #region SaveObstacles
        m_ObstacleOnScene = GameObject.FindGameObjectsWithTag(ObjectTags.ObstacleParent).ToList();
        TempLevelData.ObstaclePositions = new Vector3[m_ObstacleOnScene.Count];
        TempLevelData.ObstacleRotations = new Quaternion[m_ObstacleOnScene.Count];
        TempLevelData.ObstacleTypes = new ObstacleType[m_ObstacleOnScene.Count];
        for (int _obstacleCount = 0; _obstacleCount < m_ObstacleOnScene.Count; _obstacleCount++)
        {
            TempLevelData.ObstaclePositions[_obstacleCount] = m_ObstacleOnScene[_obstacleCount].transform.position;
            TempLevelData.ObstacleTypes[_obstacleCount] = m_ObstacleOnScene[_obstacleCount].GetComponent<Obstacle>().ObstacleType;
            TempLevelData.ObstacleRotations[_obstacleCount] = m_ObstacleOnScene[_obstacleCount].transform.rotation;
        }
        #endregion

        #region SaveCollectableCoins
        m_CollectableCoinOnScene = GameObject.FindGameObjectsWithTag(ObjectTags.CollectedCoin).ToList();
        TempLevelData.CoinPositions = new Vector3[m_CollectableCoinOnScene.Count];
        TempLevelData.CoinRotations = new Quaternion[m_CollectableCoinOnScene.Count];
        for (int _coinCount = 0; _coinCount < m_CollectableCoinOnScene.Count; _coinCount++)
        {
            TempLevelData.CoinPositions[_coinCount] = m_CollectableCoinOnScene[_coinCount].transform.position;
            TempLevelData.CoinRotations[_coinCount] = m_CollectableCoinOnScene[_coinCount].transform.rotation;
        }
        #endregion

        AssetDatabase.CreateAsset(TempLevelData, savePath);
        AssetDatabase.SaveAssets();

    }
    public void CreateRandomLevel()
    {
        List<GameObject> m_SpawnedRoads;

        ClearScene();

        #region CreateRandomRoads
        m_SpawnedRoads = new List<GameObject>();
        for (int _spawnedRoadCount = 0; _spawnedRoadCount < m_RoadCount; _spawnedRoadCount++)
        {
            m_SpawnedRoads.Add(Instantiate(m_RoadPrefab, Vector3.zero, Quaternion.identity, null));
            Vector3 m_TempSpawnedRoadEuler;
            m_TempSpawnedRoadEuler = Vector3.zero;
            if (_spawnedRoadCount != 0)
            {
                if (_spawnedRoadCount != (m_RoadCount - 1))
                {
                    if (m_SpawnedRoads[_spawnedRoadCount - 1].transform.localEulerAngles.x == 0.0f)
                    {
                        int m_TempRandomEuler = Random.Range(0, 3);
                        m_TempSpawnedRoadEuler.x = (m_TempRandomEuler == 0) ? (-30.0f) : (30.0f);
                    }
                }
                m_SpawnedRoads[_spawnedRoadCount].transform.eulerAngles = m_TempSpawnedRoadEuler;
                m_SpawnedRoads[_spawnedRoadCount].transform.position = m_SpawnedRoads[_spawnedRoadCount - 1].GetComponent<Road>().LastPosition.position;
            }

        }
        #endregion

        #region CreateRandomObstacles

        for (int _spawnedObstacleCount = 1; _spawnedObstacleCount < m_RoadCount - 1; _spawnedObstacleCount++)
        {
            int m_TempRandomObstacleCount = Random.Range(0, 3);
            Vector3 m_TempObstaclePos = Vector3.zero;
            GameObject m_SpawnedObstacle = null;

            if (m_TempRandomObstacleCount == 0)
            {
                m_SpawnedObstacle = Instantiate(m_TurningObstaclePrefab, Vector3.zero, Quaternion.identity, m_SpawnedRoads[_spawnedObstacleCount].transform);
                m_TempObstaclePos.x = Random.Range(-7.5f, 7.5f);
                m_TempObstaclePos.z = Random.Range(2.5f, 17.5f);
                m_TempObstaclePos.y = 0.0f;
                m_SpawnedObstacle.transform.localEulerAngles = Vector3.zero;
            }
            else if (m_TempRandomObstacleCount == 1)
            {
                m_SpawnedObstacle = Instantiate(m_AxeObstaclePrefab, Vector3.zero, Quaternion.identity, m_SpawnedRoads[_spawnedObstacleCount].transform);
                int m_TempRandomAxePosSide = Random.Range(0, 2);
                m_TempObstaclePos.x = 10.5f;
                m_TempObstaclePos.z = Random.Range(0.0f, 20.0f);
                m_TempObstaclePos.y = 0.0f;
                m_SpawnedObstacle.transform.localEulerAngles = Vector3.up * (-90.0f);
                if (m_TempRandomAxePosSide == 0)
                {
                    m_TempObstaclePos.x *= -1.0f;
                    m_SpawnedObstacle.transform.localEulerAngles = Vector3.up * (90.0f);
                }
            }
            else if (m_TempRandomObstacleCount == 2)
            {
                m_SpawnedObstacle = Instantiate(m_SlidingObstaclePrefab, Vector3.zero, Quaternion.identity, m_SpawnedRoads[_spawnedObstacleCount].transform);
                m_TempObstaclePos = Vector3.zero;
                m_TempObstaclePos.z = Random.Range(2.5f, 17.5f);
                m_SpawnedObstacle.transform.localEulerAngles = Vector3.zero;
            }

            m_SpawnedObstacle.transform.localPosition = m_TempObstaclePos;
        }
        #endregion

        #region CreateRandomCoins
        for (int _spawnedCoinCount = 0; _spawnedCoinCount < 9; _spawnedCoinCount++)
        {
            GameObject m_SpawnedCoin = null;
            Transform m_SpawnedCoinParent;
            Vector3 m_SpawnedCoinLocalPos;
            m_SpawnedCoinParent = m_SpawnedRoads[Random.Range(1, (m_SpawnedRoads.Count - 1))].transform;
            m_SpawnedCoin = Instantiate(m_CollectableCoinPrefab, Vector3.zero, Quaternion.identity, m_SpawnedCoinParent);
            m_SpawnedCoinLocalPos.x = Random.Range(-7.5f, 7.5f);
            m_SpawnedCoinLocalPos.y = 0.0f;
            m_SpawnedCoinLocalPos.z = Random.Range(2.5f, 17.5f);
            m_SpawnedCoin.transform.localPosition = m_SpawnedCoinLocalPos;

        }
        #endregion
    }

    public void LoadData()
    {
        ClearScene();

        for (int _obstacleCount = 0; _obstacleCount < TempLevelData.ObstacleTypes.Length; _obstacleCount++)
        {
            GameObject m_SpawnedObstacle = null;
            switch (TempLevelData.ObstacleTypes[_obstacleCount])
            {
                case (ObstacleType.AxeObstacle):
                    m_SpawnedObstacle = Instantiate(m_AxeObstaclePrefab, TempLevelData.ObstaclePositions[_obstacleCount], Quaternion.identity, null);
                    break;
                case (ObstacleType.SlidingObstacle):
                    m_SpawnedObstacle = Instantiate(m_SlidingObstaclePrefab, TempLevelData.ObstaclePositions[_obstacleCount], Quaternion.identity, null);
                    break;
                case (ObstacleType.TurningObstacle):
                    m_SpawnedObstacle = Instantiate(m_TurningObstaclePrefab, TempLevelData.ObstaclePositions[_obstacleCount], Quaternion.identity, null);
                    break;
            }

            m_SpawnedObstacle.transform.rotation = TempLevelData.ObstacleRotations[_obstacleCount];
        }

        for (int _roadCount = 0; _roadCount < TempLevelData.RoadPositions.Length; _roadCount++)
        {
            Instantiate(m_RoadPrefab, TempLevelData.RoadPositions[_roadCount], TempLevelData.RoadRotations[_roadCount], null);
        }
        for (int _coinCount = 0; _coinCount < TempLevelData.CoinPositions.Length; _coinCount++)
        {
            Instantiate(m_CollectableCoinPrefab, TempLevelData.CoinPositions[_coinCount], TempLevelData.CoinRotations[_coinCount], null);
        }
    }

    private List<GameObject> m_RoadsOnScene;
    private List<GameObject> m_ObstacleOnScene;
    private List<GameObject> m_CollectableCoinOnScene;
    public void ClearScene()
    {
        m_RoadsOnScene = GameObject.FindGameObjectsWithTag(ObjectTags.Road).ToList();
        m_RoadsOnScene.ForEach(x =>
            {
                DestroyImmediate(x);
            });

        m_ObstacleOnScene = GameObject.FindGameObjectsWithTag(ObjectTags.ObstacleParent).ToList();
        m_ObstacleOnScene.ForEach(x =>
            {
                DestroyImmediate(x);
            });
        m_CollectableCoinOnScene = GameObject.FindGameObjectsWithTag(ObjectTags.CollectedCoin).ToList();
        m_CollectableCoinOnScene.ForEach(x =>
            {
                DestroyImmediate(x);
            });
    }
}
#endif

