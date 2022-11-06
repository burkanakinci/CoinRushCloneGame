using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : CustomBehaviour
{
    #region Fields
    [SerializeField] private Transform m_LastPosition;
    #endregion
    #region ExternalAccess
    public Transform LastPosition => m_LastPosition;
    #endregion

    public override void Initialize()
    {

    }
}
