using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    public float gameSpeed = 5f;
    public float noteSpeed = 1.6f;
    public int curCombo = 0;
    public int totalNote = 0;
    public int totalRate = 0;
    public float averageRate
    {
        get
        {
            return totalRate * 1f / totalNote;
        }
    }
}
