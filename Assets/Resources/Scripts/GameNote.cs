using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNote : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * 0.001f * GameMgr.Inst.gameInfo.gameSpeed * GameMgr.Inst.gameInfo.noteSpeed);
    }
}