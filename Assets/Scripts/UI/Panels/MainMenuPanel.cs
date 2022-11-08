using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuPanel : UIPanel
{
    [SerializeField] private TextMeshProUGUI m_LevelText;
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);
        GameManager.Instance.OnResetToMainMenu += ShowPanel;
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        m_LevelText.text = "Level : " + GameManager.Instance.PlayerManager.GetLevelNumber().ToString();
    }

}
