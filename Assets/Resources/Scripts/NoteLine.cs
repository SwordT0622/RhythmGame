using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteLine : MonoBehaviour
{
    [SerializeField] Note[] m_Notes = null;

    public void Initialize(int[] noteInfo)
    {
        for(int i = 0; i < m_Notes.Length; i++)
        {
            m_Notes[i].SetNote(noteInfo[i]);
        }
    }

    public int[] GetNoteInfos()
    {
        int[] noteInfo = new int[4];

        for(int i = 0; i < m_Notes.Length; i++)
        {
            noteInfo[i] = m_Notes[i].curIdx;
        }

        return noteInfo;
    }
}
