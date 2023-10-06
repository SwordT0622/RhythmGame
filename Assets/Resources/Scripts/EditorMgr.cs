using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteType
{
    None = -1,
    Short = 0,
    Slide = 1,
    Double = 2
}

public class EditorMgr
{
    public static EditorMgr _Inst = null;
    public static EditorMgr Inst
    {
        get
        {
            if(_Inst == null )
                _Inst = new EditorMgr();
            return _Inst;
        }
    }

    public EditorInfo editorInfo = new EditorInfo();
}
