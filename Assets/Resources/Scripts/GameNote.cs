using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNote : MonoBehaviour
{
    float breakY = 0;

    public void Initialize(float breakY)
    {
        this.breakY = breakY;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * 0.001f * GameMgr.Inst.gameInfo.gameSpeed * GameMgr.Inst.gameInfo.noteSpeed);

        if(transform.localPosition.y < breakY)
        {
            Debug.Log("¹Ì½º");
            GameMgr.Inst.gameInfo.totalRate += 0;
            GameMgr.Inst.gameInfo.totalNote += 1;
            GameMgr.Inst.gameInfo.curCombo = 0;
            Destroy(gameObject);
        }
    }
}