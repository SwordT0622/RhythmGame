using System.Collections;
using System.Collections.Generic;
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

                }

            }
        }
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

    void MoveCamera(float v)
    {
        m_Camera.transform.position = new Vector2(0, v);
    }
}
