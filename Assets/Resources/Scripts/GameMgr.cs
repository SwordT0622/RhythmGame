using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr
{
    public static GameMgr _Inst = null;
    public static GameMgr Inst
    {
        get
        {
            if (_Inst == null)
                _Inst = new GameMgr();
            return _Inst;
        }
    }

    public GameInfo gameInfo = new GameInfo();
}
