using System;
using System.Collections;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    protected CanvasGroup m_CanvasGroup;
    private UIManager m_UIManager;

    public virtual void Initialize(UIManager uiManager)
    {
        m_UIManager = uiManager;
        m_CanvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void ShowPanel()
    {
        m_UIManager.HideAllPanels();

        m_CanvasGroup.Open();
        SetCurrentPanel();
    }

    public virtual void HidePanel()
    {
        m_CanvasGroup.Close();
    }

    public virtual void SetCurrentPanel()
    {
        m_UIManager.SetCurrentUIPanel(this);
    }
}
