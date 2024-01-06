using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class GameGame : MonoBehaviour
{
    const float RATELINE = -3.288f;
    [SerializeField] Transform m_NoteParent = null;
    [SerializeField] GameObject[] m_NotePrefabs = null;
    [SerializeField] Transform[] m_NoteLines = null;
    [SerializeField] VideoPlayer m_BGAPlayer = null;
    [SerializeField] MeshRenderer m_Mesh = null;
    [SerializeField] Material[] m_Materials = null;

    public bool[] isHoleLine = new bool[4];

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        m_Mesh.material = m_Materials[0];
        string pathName = "Assets/Resources/Patterns/" + GameMgr.Inst.gameInfo.curSongDifference + ".txt";
        FileStream fs = new FileStream(pathName, FileMode.Open);
        StreamReader sr = new StreamReader(fs);

        float spacing = float.Parse(sr.ReadLine()) / 5;
        float noteSpeed = float.Parse(sr.ReadLine()) / 5;
        float gameSpeed = GameMgr.Inst.gameInfo.gameSpeed;

        int count = int.Parse(sr.ReadLine());
        for (int i = 0; i < count; i++)
        {
            string Line = sr.ReadLine();
            string[] data = Line.Split('\t');

            for(int j = 0; j < data.Length; j++)
            {
                if (int.Parse(data[j]) != -1)
                {
                    CreateNote(int.Parse(data[j]), j, i * spacing * gameSpeed + (4 * gameSpeed * noteSpeed), RATELINE - (0.15f * gameSpeed * noteSpeed));
                }
            }

            sr.ReadLine();
        }

        GameMgr.Inst.gameInfo.totalNoteCount = int.Parse(sr.ReadLine());

        sr.Close();
        fs.Close();
        GameMgr.Inst.gameInfo.noteSpace = spacing;
        GameMgr.Inst.gameInfo.noteSpeed = noteSpeed;

        for (int i = 0; i < m_NoteLines.Length; i++)
        {
            m_NoteLines[i].position = new Vector3(m_NoteLines[i].position.x, m_NoteLines[i].position.y - (0.25f * gameSpeed * noteSpeed), 0);
        }

        m_BGAPlayer.clip = MusicMgr.Inst.m_BGAs[GameMgr.Inst.gameInfo.curSongIdx];
        StartCoroutine(IEnum_Play());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GetNote(0);
            isHoleLine[0] = true;
        }
        if( Input.GetKeyDown(KeyCode.D))
        {
            GetNote(1);
            isHoleLine[1] = true;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GetNote(2);
            isHoleLine[2] = true;
        }
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            GetNote(3);
            isHoleLine[3] = true;
        }



        if (Input.GetKeyUp(KeyCode.S))
        {
            isHoleLine[0] = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            isHoleLine[1] = false;
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            isHoleLine[2] = false;
        }
        if (Input.GetKeyUp(KeyCode.Semicolon))
        {
            isHoleLine[3] = false;
        }
    }

    void GetNote(int line)
    {
        RaycastHit hit;
        float gameSpeed = GameMgr.Inst.gameInfo.gameSpeed;
        float noteSpeed = GameMgr.Inst.gameInfo.noteSpeed;
        float dis = 0;

        Vector3 startPos = m_NoteLines[line].position;

        int layerMask = 1 << LayerMask.NameToLayer("Note");

        if (Physics.Raycast(startPos, Vector3.up, out hit, 0.5f * gameSpeed * noteSpeed, layerMask))
        {
            if(hit.transform.position.y > RATELINE)
                dis = hit.transform.position.y - RATELINE;
            else
                dis = RATELINE - hit.transform.position.y;

            if (hit.collider.tag == "Note0")
            {
                CheckRate(dis / gameSpeed / noteSpeed);
                Destroy(hit.collider.gameObject);
                return;
            }
            else if (hit.collider.tag == "Note1")
            {
                return;
            }
            else if (hit.collider.tag == "Note2")
            {
                CheckRate(dis / gameSpeed / noteSpeed);

                CreateNote(0, line, hit.transform.localPosition.y + GameMgr.Inst.gameInfo.doubleNoteValue * GameMgr.Inst.gameInfo.noteSpace * GameMgr.Inst.gameInfo.gameSpeed * GameMgr.Inst.gameInfo.noteSpeed, RATELINE - (0.15f * gameSpeed * noteSpeed));
                Destroy(hit.collider.gameObject);
                return;
            }
        }
    }

    void CreateNote(int noteType, int line, float yPos, float breakY)
    {
        GameObject go = Instantiate(m_NotePrefabs[noteType], m_NoteParent);
        go.transform.localPosition = new Vector3(m_NoteLines[line].localPosition.x, yPos, 0);

        GameNote gameNote = go.GetComponent<GameNote>();
        gameNote.Initialize(breakY);
    }

    void CheckRate(float dis)
    {
        switch (dis)
        {
            case < 0.04f:
                {
                    Debug.Log("ÆÛÆåÆ®");
                    GameMgr.Inst.gameInfo.totalRate += 100;
                    GameMgr.Inst.gameInfo.totalNote += 1;
                    GameMgr.Inst.gameInfo.curCombo += 1;
                    break;
                }
            case < 0.08f:
                {
                    Debug.Log("Äð");
                    GameMgr.Inst.gameInfo.totalRate += 70;
                    GameMgr.Inst.gameInfo.totalNote += 1;
                    GameMgr.Inst.gameInfo.curCombo += 1;
                    break;
                }
            case < 0.12f:
                {
                    Debug.Log("±»");
                    GameMgr.Inst.gameInfo.totalRate += 40;
                    GameMgr.Inst.gameInfo.totalNote += 1;
                    GameMgr.Inst.gameInfo.curCombo += 1;
                    break;
                }
            case < 0.15f:
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
    }

    public void Game_Result()
    {
        MusicMgr.Inst.m_Musics[GameMgr.Inst.gameInfo.curSongIdx].Stop();
        m_BGAPlayer.Stop();
    }

    IEnumerator IEnum_Play()
    {
        MusicMgr.Inst.m_Musics[GameMgr.Inst.gameInfo.curSongIdx].PlayDelayed(4);
        yield return new WaitForSeconds(4);

        m_BGAPlayer.Play();
        m_Mesh.material = m_Materials[1];
    }
}
