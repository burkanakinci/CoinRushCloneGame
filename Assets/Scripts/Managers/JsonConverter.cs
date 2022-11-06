using UnityEngine;
public class JsonConverter : CustomBehaviour
{
    public PlayerData PlayerData;

    public override void Initialize()
    {
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        var data = PlayerPrefs.GetString(Constants.PLAYER_DATA);

        if (string.IsNullOrEmpty(data))
        {

            PlayerData = new PlayerData
            {
                LevelNumber = 1,
                TotalCoinCount = 0,
            };

            SavePlayerData();
        }
        else
        {
            PlayerData = JsonUtility.FromJson<PlayerData>(data);
        }
    }

    public void SavePlayerData()
    {
        var jsonData = JsonUtility.ToJson(PlayerData);
        PlayerPrefs.SetString(Constants.PLAYER_DATA, jsonData);
        PlayerPrefs.Save();
    }
}
