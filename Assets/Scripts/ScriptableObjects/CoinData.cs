using UnityEngine;

[CreateAssetMenu(fileName = "CoinData", menuName = "Coin Data")]
public class CoinData : ScriptableObject
{
    #region Attributes
    [SerializeField] private float m_RotateFrontCoinLerp = 50.0f;
    [SerializeField] private float m_MoveFrontCoinLerp = 50.0f;
    #endregion

    #region ExternalAccess
    public float RotateFrontCoinLerp => m_RotateFrontCoinLerp;
    public float MoveFrontCoinLerp => m_MoveFrontCoinLerp;
    #endregion
}
