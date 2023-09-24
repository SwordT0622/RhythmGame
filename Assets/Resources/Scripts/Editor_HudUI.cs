using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Editor_HudUI : MonoBehaviour
{
    [SerializeField] EditorGame m_Game = null;
    [SerializeField] InputField m_NoteCountInput = null;
    [SerializeField] Button m_AddBtn = null;
    [SerializeField] Slider m_SpacingSlider = null;
    [SerializeField] Slider m_SpeedSlider = null;
    [SerializeField] Text m_SpacingTxt = null;
    [SerializeField] Text m_SpeedTxt = null;

    [SerializeField] Slider m_NoteLineSlider = null;

    public delegate void DelegateFunc(int count);
    public DelegateFunc OnDelegate = null;

    public delegate void DelegateFunc2(float value);
    public DelegateFunc2 OnDelegate2 = null;

    private void Start()
    {
        m_AddBtn.onClick.AddListener(OnClicked_Add);
        m_SpacingSlider.onValueChanged.AddListener(OnValueChanged_Spacing);
        m_SpeedSlider.onValueChanged.AddListener(OnValueChanged_Speed);
        m_NoteLineSlider.onValueChanged.AddListener(OnValueChanged_NoteLine);
    }

    void OnValueChanged_NoteLine(float v)
    {
        if (OnDelegate2 != null)
            OnDelegate2(v);
    }

    void OnClicked_Add()
    {
        int count = int.Parse(m_NoteCountInput.text);

        if (OnDelegate != null)
            OnDelegate(count);

        m_NoteCountInput.text = string.Empty;
    }

    void OnValueChanged_Spacing(float v)
    {
        EditorMgr.Inst.editorInfo.spacing = v / 10;
        m_SpacingTxt.text = string.Format("{0:0.0}", v / 10);
    }

    void OnValueChanged_Speed(float v)
    {
        EditorMgr.Inst.editorInfo.speed = v / 10;
        m_SpeedTxt.text = string.Format("{0:0.0}", v / 10);
    }
}