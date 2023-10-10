using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRateLine : MonoBehaviour
{
    [SerializeField] GameGame m_Game = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Note1")
        {
            int line = 0;

            switch (other.transform.localPosition.x)
            {
                case -3:
                    {
                        line = 0;
                        break;
                    }
                case -1:
                    {
                        line = 1;
                        break;
                    }
                case 1:
                    {
                        line = 2;
                        break;
                    }
                case 3:
                    {
                        line = 3;
                        break;
                    }
            }

            if (m_Game.isHoleLine[line])
            {
                Debug.Log("∆€∆Â∆Æ");
                GameMgr.Inst.gameInfo.totalRate += 100;
                GameMgr.Inst.gameInfo.totalNote += 1;
                GameMgr.Inst.gameInfo.curCombo += 1;
                Destroy(other.gameObject);
            }
        }
    }
}
