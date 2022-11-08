using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudPanel : UIPanel
{
    [SerializeField] private TextMeshProUGUI m_LevelText;
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);

    }

    private void OnDestroy()
    {
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        m_LevelText.text = "Level : " + GameManager.Instance.PlayerManager.GetLevelNumber().ToString();
    }

}
