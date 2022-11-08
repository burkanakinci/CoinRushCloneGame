using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : CustomBehaviour
{
    #region Attributes
    public MainCoin MainCoin;
    public Coin LastCoin;
    #endregion
    #region ExternalAccess

    #endregion
    public override void Initialize()
    {
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += OnLevelFailed;

        MainCoin.Initialize();
        LastCoin = MainCoin;

    }

    public void UpdateTotalCoinCountData(int _coinCount)
    {
        GameManager.Instance.JsonConverter.PlayerData.TotalCoinCount = _coinCount;
        GameManager.Instance.JsonConverter.SavePlayerData();
    }
    public int GetTotalCoinCount()
    {
        return GameManager.Instance.JsonConverter.PlayerData.TotalCoinCount;
    }
    public void UpdateLevelData(int _levelNumber)
    {
        GameManager.Instance.JsonConverter.PlayerData.LevelNumber = _levelNumber;
        GameManager.Instance.JsonConverter.SavePlayerData();
    }


    public int GetLevelNumber()
    {
        return GameManager.Instance.JsonConverter.PlayerData.LevelNumber;
    }

    #region Events

    private void OnResetToMainMenu()
    {
        LastCoin = MainCoin;
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
