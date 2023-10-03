using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EditorGame : MonoBehaviour
{
    [SerializeField] EditorHudUI m_EditorHudUI = null;
    [SerializeField] GameObject m_NotePref = null;
    [SerializeField] Transform m_NoteParent = null;
    [SerializeField] Transform m_Note = null;

    List<NoteLine> m_NoteLines = new List<NoteLine>();

    private void Start()
    {
        m_EditorHudUI.OnDelegate = new EditorHudUI.DelegateFunc(CreateNotes);
        m_EditorHudUI.OnDelegate2 = new EditorHudUI.DelegateFunc2(PlayNote);
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
        int nCount = m_NoteLines.Count;
        for (int i = nCount; i < nCount + count; i++)
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

    void PlayNote(float v)
    {
        m_Note.position = new Vector3(0, -3.288f - v, 0);
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
        FileStream fs = new FileStream(pathName, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);

        sw.WriteLine(EditorMgr.Inst.editorInfo.noteCount);
        for (int i = 0; i < m_NoteLines.Count; i++)
        {
            int[] info = m_NoteLines[i].GetNoteInfos();

            sw.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\n", info[0], info[1], info[2], info[3]));
        }

        sw.WriteLine(EditorMgr.Inst.editorInfo.spacing);
        sw.WriteLine(EditorMgr.Inst.editorInfo.speed);

        sw.Close();
        fs.Close();
    }

    public void Load(string pathName)
    {
        FileStream fs = new FileStream(pathName, FileMode.Open);
        StreamReader sr = new StreamReader(fs);

        int count = int.Parse(sr.ReadLine());
        for(int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(m_NotePref, m_NoteParent);
            go.transform.localPosition = new Vector3(0, i * EditorMgr.Inst.editorInfo.spacing, 0);

            string Line = sr.ReadLine();
            string[] data = Line.Split('\t');

            int[] noteInfo = new int[] { int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]) };

            NoteLine noteLine = go.GetComponent<NoteLine>();
            noteLine.Initialize(noteInfo);
            m_NoteLines.Add(noteLine);
            sr.ReadLine();
        }

        float spacing = float.Parse(sr.ReadLine());
        float speed = float.Parse(sr.ReadLine());

        sr.Close();
        fs.Close();

        EditorMgr.Inst.editorInfo.noteCount = count;
        EditorMgr.Inst.editorInfo.spacing = spacing;
        EditorMgr.Inst.editorInfo.speed = speed;
        m_EditorHudUI.SetSliders();
    }
}
