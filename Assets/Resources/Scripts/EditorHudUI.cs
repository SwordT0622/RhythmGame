using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EditorHudUI : MonoBehaviour
{
    [SerializeField] EditorGame m_Game = null;
    [SerializeField] InputField m_NoteCountInput = null;
    [SerializeField] InputField m_PatternNameInput = null;
    [SerializeField] Button m_AddBtn = null;
    [SerializeField] Button m_SaveBtn = null;
    [SerializeField] Button m_LoadBtn = null;
    [SerializeField] Button m_PlayBtn = null;
    [SerializeField] Button m_StopBtn = null;
    [SerializeField] Slider m_SpacingSlider = null;
    [SerializeField] Slider m_SpeedSlider = null;
    [SerializeField] Text m_SpacingTxt = null;
    [SerializeField] Text m_SpeedTxt = null;

    [SerializeField] Slider m_NoteLineSlider = null;

    [SerializeField] AudioSource m_CurMusic = null;

    public delegate void DelegateFunc(int count);
    public DelegateFunc OnDelegate = null;

    public delegate void DelegateFunc2(float value);
    public DelegateFunc2 OnDelegate2 = null;


    bool isPlay = false;

    private void Start()
    {
        m_AddBtn.onClick.AddListener(OnClicked_Add);
        m_SaveBtn.onClick.AddListener(OnClicked_Save);
        m_LoadBtn.onClick.AddListener(OnClicked_Load);
        m_PlayBtn.onClick.AddListener(OnClicked_Play);
        m_StopBtn.onClick.AddListener(OnClicked_Stop);
        m_SpacingSlider.onValueChanged.AddListener(OnValueChanged_Spacing);
        m_SpeedSlider.onValueChanged.AddListener(OnValueChanged_Speed);
        m_NoteLineSlider.onValueChanged.AddListener(OnValueChanged_NoteLine);
    }

    private void FixedUpdate()
    {
        if (isPlay)
        {
            m_NoteLineSlider.value += 0.001f * EditorMgr.Inst.editorInfo.speed;
        }
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
        m_NoteLineSlider.maxValue = EditorMgr.Inst.editorInfo.noteCount * EditorMgr.Inst.editorInfo.spacing;
    }

    void OnClicked_Save()
    {
        string pathName = "Assets/Resources/Patterns/" + m_PatternNameInput.text + ".txt";
        m_Game.Save(pathName);
    }

    void OnClicked_Load()
    {
        string pathName = "Assets/Resources/Patterns/" + m_PatternNameInput.text + ".txt";
        m_Game.Load(pathName);
    }

    void OnClicked_Play()
    {
        m_CurMusic.Play();
        m_NoteLineSlider.value = 0;
        isPlay = true;
    }

    void OnClicked_Stop()
    {
        m_CurMusic.Stop();
        isPlay = false;
    }

    void OnValueChanged_Spacing(float v)
    {
        EditorMgr.Inst.editorInfo.spacing = v / 100;
        m_SpacingTxt.text = string.Format("{0:0.00}", v / 100);

        m_Game.MoveNote();
        m_NoteLineSlider.maxValue = EditorMgr.Inst.editorInfo.noteCount * EditorMgr.Inst.editorInfo.spacing;
    }

    void OnValueChanged_Speed(float v)
    {
        EditorMgr.Inst.editorInfo.speed = v / 100;
        m_SpeedTxt.text = string.Format("{0:0.00}", v / 100);
    }

    public void SetSliders()
    {
        m_NoteLineSlider.maxValue = EditorMgr.Inst.editorInfo.noteCount * EditorMgr.Inst.editorInfo.spacing;
        m_SpacingSlider.value = EditorMgr.Inst.editorInfo.spacing * 100;
        m_SpeedSlider.value = EditorMgr.Inst.editorInfo.speed * 100;
    }
}