using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGame : MonoBehaviour
{
    [SerializeField] Transform[] m_NoteLines = null;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            RaycastHit hit;
            float gameSpeed = GameMgr.Inst.gameInfo.gameSpeed;
            float noteSpeed = GameMgr.Inst.gameInfo.noteSpeed;

            if (Physics.Raycast(m_NoteLines[0].position, Vector3.up, out hit, 0.12f * gameSpeed * noteSpeed))
            {
                if(hit.collider.tag == "Note0")
                {
                    float dis = hit.transform.position.y - m_NoteLines[0].position.y;

                    switch (dis / gameSpeed / noteSpeed)
                    {
                        case < 0.04f:
                            {
                                Debug.Log("ÆÛÆåÆ®");
                                GameMgr.Inst.gameInfo.totalRate += 100;
                                GameMgr.Inst.gameInfo.totalNote += 1;
                                GameMgr.Inst.gameInfo.curCombo += 1;
                                break;
                            }
                        case < 0.06f:
                            {
                                Debug.Log("Äð");
                                GameMgr.Inst.gameInfo.totalRate += 70;
                                GameMgr.Inst.gameInfo.totalNote += 1;
                                GameMgr.Inst.gameInfo.curCombo += 1;
                                break;
                            }
                        case < 0.08f:
                            {
                                Debug.Log("±»");
                                GameMgr.Inst.gameInfo.totalRate += 40;
                                GameMgr.Inst.gameInfo.totalNote += 1;
                                GameMgr.Inst.gameInfo.curCombo += 1;
                                break;
                            }
                        case < 0.1f:
                            {
                                Debug.Log("¹èµå");
                                GameMgr.Inst.gameInfo.totalRate += 10;
                                GameMgr.Inst.gameInfo.totalNote += 1;
                                GameMgr.Inst.gameInfo.curCombo += 1;
                                break;
                            }
                        default:
                            {
                                Debug.Log("¹Ì½º");
                                GameMgr.Inst.gameInfo.totalRate += 0;
                                GameMgr.Inst.gameInfo.totalNote += 1;
                                GameMgr.Inst.gameInfo.curCombo = 0;
                                break;
                            }
                    }

                    Destroy(hit.collider.gameObject);
                    return;
                }
            }

            if(Physics.Raycast(m_NoteLines[0].position, Vector3.down, out hit, 0.06f * GameMgr.Inst.gameInfo.gameSpeed * GameMgr.Inst.gameInfo.noteSpeed))
            {
                if(hit.collider.tag == "Note0")
                {
                    float dis = m_NoteLines[0].position.y - hit.transform.position.y;

                    switch (dis / gameSpeed / noteSpeed)
                    {
                        case < 0.04f:
                            {
                                Debug.Log("ÆÛÆåÆ®");
                                GameMgr.Inst.gameInfo.totalRate += 100;
                                GameMgr.Inst.gameInfo.totalNote += 1;
                                GameMgr.Inst.gameInfo.curCombo += 1;
                                break;
                            }
                        case < 0.06f:
                            {
                                Debug.Log("Äð");
                                GameMgr.Inst.gameInfo.totalRate += 70;
                                GameMgr.Inst.gameInfo.totalNote += 1;
                                GameMgr.Inst.gameInfo.curCombo += 1;
                                break;
                            }
                        case < 0.08f:
                            {
                                Debug.Log("±»");
                                GameMgr.Inst.gameInfo.totalRate += 40;
                                GameMgr.Inst.gameInfo.totalNote += 1;
                                GameMgr.Inst.gameInfo.curCombo += 1;
                                break;
                            }
                        case < 0.1f:
                            {
                                Debug.Log("¹èµå");
                                GameMgr.Inst.gameInfo.totalRate += 10;
                                GameMgr.Inst.gameInfo.totalNote += 1;
                                GameMgr.Inst.gameInfo.curCombo += 1;
                                break;
                            }
                        default:
                            {
                                Debug.Log("¹Ì½º");
                                GameMgr.Inst.gameInfo.totalRate += 0;
                                GameMgr.Inst.gameInfo.totalNote += 1;
                                GameMgr.Inst.gameInfo.curCombo = 0;
                                break;
                            }
                    }

                    Destroy(hit.collider.gameObject);
                    return;
                }
            }
        }
    }
}
