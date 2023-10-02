using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EditorGame : MonoBehaviour
{
    [SerializeField] Camera m_Camera = null;
    [SerializeField] Editor_HudUI m_EditorHudUI = null;
    [SerializeField] GameObject m_NotePref = null;
    [SerializeField] Transform m_NoteParent = null;

    List<NoteLine> m_NoteLines = new List<NoteLine>();

    private void Start()
    {
        m_EditorHudUI.OnDelegate = new Editor_HudUI.DelegateFunc(CreateNotes);
        m_EditorHudUI.OnDelegate2 = new Editor_HudUI.DelegateFunc2(MoveCamera);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if(hit.transform.tag == "Note")
                {
                    Note note = hit.transform.GetComponent<Note>();
                    note.SetNote(0);

                    Debug.Log("≥Î∆Æ");
                }
            }
        }
    }

    void CreateNotes(int count)
    {
        for (int i = 0; i < m_NoteLines.Count; i++)
            Destroy(m_NoteLines[i].gameObject);

        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(m_NotePref, m_NoteParent);
            go.transform.localPosition = new Vector3(0, i * EditorMgr.Inst.editorInfo.spacing, 0);

            int[] noteInfo = new int[] { -1, -1, -1, -1 };

            NoteLine noteLine = go.GetComponent<NoteLine>();
            noteLine.Initialize(noteInfo);
            m_NoteLines.Add(noteLine);
        }

        EditorMgr.Inst.editorInfo.noteCount = m_NoteLines.Count;
    }

    void MoveCamera(float v)
    {
        m_Camera.transform.position = new Vector3(0, v, -9.74f);
    }
    
    public void MoveNote()
    {
        for(int i = 0; i < m_NoteLines.Count; i++)
        {
            m_NoteLines[i].transform.localPosition = new Vector3(0, i * EditorMgr.Inst.editorInfo.spacing, 0);
        }
    }

    public void Save(string pathName)
    {
        StreamWriter sw = new StreamWriter(pathName);

        for(int i = 0; i < m_NoteLines.Count; i++)
        {
            int[] info = m_NoteLines[i].GetNoteInfos();

            sw.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\n", info[0], info[1], info[2], info[3]));
        }

        sw.WriteLine(EditorMgr.Inst.editorInfo.noteCount);
        sw.WriteLine(EditorMgr.Inst.editorInfo.spacing);
        sw.WriteLine(EditorMgr.Inst.editorInfo.speed);

        sw.Close();
    }
}
