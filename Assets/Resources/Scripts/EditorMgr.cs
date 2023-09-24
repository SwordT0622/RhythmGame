using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
