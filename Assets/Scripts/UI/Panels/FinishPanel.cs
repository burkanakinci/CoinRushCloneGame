using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FinishPanel : UIPanel
{
    [Header("Fail")]
    [SerializeField] private CanvasGroup m_FailCanvas;
    [SerializeField] private int m_FailReward = 100;

    [Header("Success")]
    [SerializeField] private CanvasGroup m_SuccessCanvas;
    [SerializeField] private int m_SuccessReward = 300;
    [Header("Tween")]
    [SerializeField] private float m_RewardTweenDuration = 3.0f;
    [SerializeField] private AnimationCurve m_RewardTweenCurve;
    [SerializeField] private float m_StartTweenDelay = 2.0f;
    private int m_CurrentRewardCoinCount;
    [SerializeField] private TextMeshProUGUI m_CoinCountText;
    [SerializeField] private TextMeshProUGUI m_RewardCoinText;
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);

        m_RewardCoinTweenID = GetInstanceID() + "m_RewardCoinTweenID";

        GameManager.Instance.OnLevelCompleted += ShowSuccessPanel;
        GameManager.Instance.OnLevelFailed += ShowFailPanel;
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnLevelCompleted += ShowPanel;
        GameManager.Instance.OnLevelFailed += ShowPanel;
    }

    private Coroutine m_ShowFinishPanelCoroutine;
    public override void ShowPanel()
    {
        if (m_ShowFinishPanelCoroutine != null)
        {
            StopCoroutine(m_ShowFinishPanelCoroutine);
        }

        m_ShowFinishPanelCoroutine = StartCoroutine(ShowFinishPanelCoroutine());
    }
    private IEnumerator ShowFinishPanelCoroutine()
    {
        yield return new WaitUntil(() => ((GameManager.Instance.PlayerManager.LastCoin.CompletedLastSequence) ||
            (GameManager.Instance.PlayerManager.MainCoin.EqualMainCoinState(MainCoinStates.FallMainCoinState))));
        base.ShowPanel();

        StartRewardCoinTween();
    }

    private void SetFinishTexts()
    {
        m_RewardCoinText.text = m_CurrentRewardCoinCount.ToString();
        m_CoinCountText.text = GameManager.Instance.PlayerManager.GetTotalCoinCount().ToString();
    }

    private Coroutine m_ShowSuccessPanelCoroutine;
    private void ShowSuccessPanel()
    {
        if (m_ShowSuccessPanelCoroutine != null)
        {
            StopCoroutine(m_ShowSuccessPanelCoroutine);
        }

        m_ShowSuccessPanelCoroutine = StartCoroutine(ShowSuccessPanelCoroutine());
    }
    private void ShowFailPanel()
    {
        if (m_ShowSuccessPanelCoroutine != null)
        {
            StopCoroutine(m_ShowSuccessPanelCoroutine);
        }

        m_ShowSuccessPanelCoroutine = StartCoroutine(ShowFailPanelCoroutine());
    }
    private IEnumerator ShowSuccessPanelCoroutine()
    {
        yield return new WaitUntil(() => (m_CanvasGroup.alpha == 1));
        m_CurrentRewardCoinCount = (m_SuccessReward * GameManager.Instance.Finish.FullStairIndex);
        m_FailCanvas.Close();
        m_SuccessCanvas.Open();
        SetFinishTexts();
    }
    private IEnumerator ShowFailPanelCoroutine()
    {
        yield return new WaitUntil(() => (m_CanvasGroup.alpha == 1));
        m_CurrentRewardCoinCount = m_FailReward;
        m_SuccessCanvas.Close();
        m_FailCanvas.Open();
        SetFinishTexts();
    }

    private Coroutine m_StartRewardCoinCoroutine;
    private void StartRewardCoinTween()
    {
        if (m_StartRewardCoinCoroutine != null)
        {
            StopCoroutine(m_StartRewardCoinCoroutine);
        }

        m_StartRewardCoinCoroutine = StartCoroutine(RewardCoinCoroutine());
    }
    private IEnumerator RewardCoinCoroutine()
    {
        yield return new WaitForSecondsRealtime(m_StartTweenDelay);
        RewardCoinTween();
    }
    private string m_RewardCoinTweenID;
    private float m_CoinTextLerpValue;
    private int m_StartTotalCoinCount, m_FinishTotalCoinCount;
    private int m_StartRewardCoinCount;
    private int m_TempTotalCoinCount, m_TempRewardCoinCount;
    private void RewardCoinTween()
    {
        DOTween.Kill(m_RewardCoinTweenID);
        m_CoinTextLerpValue = 0.0f;

        m_StartRewardCoinCount = m_CurrentRewardCoinCount;
        m_StartTotalCoinCount = GameManager.Instance.PlayerManager.GetTotalCoinCount();

        m_FinishTotalCoinCount = m_StartRewardCoinCount + m_StartTotalCoinCount;

        DOTween.To(() => m_CoinTextLerpValue, x => m_CoinTextLerpValue = x, 1.0f, m_RewardTweenDuration).
        OnUpdate(() =>
        {
            SetFinishPanelCoins();
        }).
        OnComplete(() =>
        {
            GameManager.Instance.PlayerManager.UpdateTotalCoinCountData(m_FinishTotalCoinCount);
        }).
        SetEase(m_RewardTweenCurve).
        SetId(m_RewardCoinTweenID);
    }

    private void SetFinishPanelCoins()
    {
        m_TempRewardCoinCount = (int)Mathf.Lerp(m_StartRewardCoinCount, 0, m_CoinTextLerpValue);
        m_TempTotalCoinCount = (int)Mathf.Lerp(m_StartTotalCoinCount, m_FinishTotalCoinCount, m_CoinTextLerpValue);

        m_RewardCoinText.text = m_TempRewardCoinCount.ToString();
        m_CoinCountText.text = m_TempTotalCoinCount.ToString();
    }

    public void ContinueButton(bool _isRestart)
    {
        if (!_isRestart)
        {
            GameManager.Instance.PlayerManager.UpdateLevelData((GameManager.Instance.PlayerManager.GetLevelNumber() + 1));
        }

        GameManager.Instance.ResetToMainMenu();
    }
    private void StopAllCoroutineOnFinihPanel()
    {
        if (m_ShowFinishPanelCoroutine != null)
        {
            StopCoroutine(m_ShowFinishPanelCoroutine);
        }
        if (m_ShowSuccessPanelCoroutine != null)
        {
            StopCoroutine(m_ShowSuccessPanelCoroutine);
        }
        if (m_StartRewardCoinCoroutine != null)
        {
            StopCoroutine(m_StartRewardCoinCoroutine);
        }
    }
    private void KillAllTween()
    {
        DOTween.Kill(m_RewardCoinTweenID);
    }
    private void OnResetToMainMenu()
    {
        KillAllTween();
        StopAllCoroutineOnFinihPanel();
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnLevelCompleted -= ShowPanel;
        GameManager.Instance.OnLevelCompleted -= ShowSuccessPanel;
        GameManager.Instance.OnLevelFailed -= ShowPanel;
        GameManager.Instance.OnLevelFailed -= ShowFailPanel;
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
    }
}
