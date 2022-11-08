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
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
    }

    private void OnDestroy()
    {
    }

    private void OnResetToMainMenu()
    {
        GameManager.Instance.InputManager.OnSwiped += ShowHudPanel;
    }

    public void ShowHudPanel(float _swipe)
    {
        if (_swipe == 0.0f)
        {
            return;
        }
        GameManager.Instance.InputManager.OnSwiped -= ShowHudPanel;
        ShowPanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        m_LevelText.text = "Level : " + GameManager.Instance.PlayerManager.GetLevelNumber().ToString();
    }

}
