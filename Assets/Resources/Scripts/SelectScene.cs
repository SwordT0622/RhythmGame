using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScene : MonoBehaviour
{
    [SerializeField] SelectHudUI m_HudUI = null;
    private void Start()
    {
        m_HudUI.Initialize();
    }
}
