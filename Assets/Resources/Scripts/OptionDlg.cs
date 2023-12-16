using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionDlg : MonoBehaviour
{
    [SerializeField] GameObject[] m_Options = null;
    [SerializeField] SpriteRenderer m_BGMVolumeSprite = null;
    [SerializeField] SpriteRenderer m_SFXVolumeSprite = null;
    [SerializeField] Sprite[] m_Volumes = null;

    int curOption = 0;

    public void Initialize()
    {
        curOption = 0;
        m_BGMVolumeSprite.sprite = m_Volumes[GameMgr.Inst.gameInfo.BGMVolume];
        m_SFXVolumeSprite.sprite = m_Volumes[GameMgr.Inst.gameInfo.SFXVolume];
    }
}