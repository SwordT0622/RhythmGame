using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MusicMgr : MonoBehaviour
{
    public static MusicMgr Inst = null;
    public AudioSource[] m_Musics = null;
    public VideoClip[] m_BGAs = null;

    private void Awake()
    {
        Inst = this;
        SetVolume();
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetVolume()
    {
        for (int i = 0; i < m_Musics.Length; i++)
        {
            m_Musics[i].volume = GameMgr.Inst.gameInfo.BGMVolume / 10f;
        }
    }
}
