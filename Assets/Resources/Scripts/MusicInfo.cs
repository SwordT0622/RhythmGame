using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInfo : MonoBehaviour
{
    public static MusicInfo Inst;
    private void Awake()
    {
        Inst = this;
    }

    public int musicLength
    {
        get
        {
            return songImgs.Length;
        }
        private set { }
    }
    public string[] songNames = null;
    public string[] compossers = null;
    public int[] songBPMs = null;
    public Sprite[] songImgs = null;
}