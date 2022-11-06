using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : CustomBehaviour
{
    public static GameManager Instance { get; private set; }
    #region Attributes
    public InputManager InputManager;
    public PlayerManager PlayerManager;
    public CameraManager CameraManager;
    public ObjectPool ObjectPool;
    public LevelManager LevelManager;
    public JsonConverter JsonConverter;
    #endregion

    #region Actions
    public event Action OnResetToMainMenu;
    public event Action OnLevelCompleted;
    public event Action OnLevelFailed;
    public event Action OnGameStart;
    #endregion
    private void Awake()
    {
        Instance = this;

        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Initialize();

        InputManager.Initialize();
        PlayerManager.Initialize();
        CameraManager.Initialize();
        ObjectPool.Initialize();
        JsonConverter.Initialize();
        LevelManager.Initialize();
    }
    public override void Initialize()
    {

    }
    private void Start()
    {
        ResetToMainMenu();
    }
    public void ResetToMainMenu()
    {
        OnResetToMainMenu?.Invoke();
    }
    public void LevelFailed()
    {
        OnLevelFailed?.Invoke();
    }
    public void LevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }
    public void StartGame()
    {
        OnGameStart?.Invoke();
    }
}
