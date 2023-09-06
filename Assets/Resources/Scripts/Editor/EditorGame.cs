using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorGame : MonoBehaviour
{
    [SerializeField] Camera m_Camera = null;
    [SerializeField] EditorHudUI m_EditorHudUI = null;
    [SerializeField] GameObject m_NotePref = null;
    [SerializeField] Transform m_NoteParent = null;

    List<NoteLine> m_NoteLines = new List<NoteLine>();

    private void Start()
    {
        m_EditorHudUI.OnDelegate = new EditorHudUI.DelegateFunc(CreateNotes);
    }

    void CreateNotes(int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(m_NotePref, m_NoteParent);
            go.transform.localPosition = new Vector3(0, m_NoteLines.Count * EditorMgr.Inst.editorInfo.spacing, 0);

            NoteLine noteLine = go.GetComponent<NoteLine>();
            m_NoteLines.Add(noteLine);
        }
    }
}
