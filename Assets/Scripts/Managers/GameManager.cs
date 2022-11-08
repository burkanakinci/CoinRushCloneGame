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
    public Finish Finish;
    public UIManager UIManager;
    #endregion

    #region Actions
    public event Action OnResetToMainMenu;
    public event Action OnLevelCompleted;
    public event Action OnLevelFailed;
    #endregion
    private void Awake()
    {
        Instance = this;

        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        PlayerPrefs.DeleteAll();
        Initialize();
    }
    public override void Initialize()
    {
        JsonConverter.Initialize();
        InputManager.Initialize();
        CameraManager.Initialize();
        ObjectPool.Initialize();
        LevelManager.Initialize();
        PlayerManager.Initialize();
        Finish.Initialize();
        UIManager.Initialize();
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
}
