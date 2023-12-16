using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectHudUI : MonoBehaviour
{
    [SerializeField] Sprite[] m_DifSelectSprites = null;
    [SerializeField] Sprite[] m_DifUnSelectSprites = null;
    [SerializeField] Image[] m_DifImgs = null;
    [SerializeField] Image m_curSongImg = null;
    [SerializeField] Text m_SongNameTxt = null;
    [SerializeField] Text m_ComposserTxt = null;
    [SerializeField] Text m_BPMTxt = null;

    [SerializeField] GameObject m_MusicInfoPref = null;
    [SerializeField] Transform m_MusingInfoParent = null;

    [SerializeField] MusicItem[] m_MusicItems = new MusicItem[9];
    bool canMove = true;
    int curSongNum = 0;
    int curSongDif = 0;

    float holdTime = 0;

    private void Update()
    {
        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartCoroutine(IEnum_MoveMusicItems(false));
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartCoroutine(IEnum_MoveMusicItems(true));
            }
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            holdTime += Time.deltaTime;
            if(holdTime >= 1)
            {
                if (canMove)
                {
                    StartCoroutine(IEnum_MoveMusicItems(false));
                }
            }
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            holdTime += Time.deltaTime;
            if (holdTime >= 1)
            {
                if (canMove)
                {
                    StartCoroutine(IEnum_MoveMusicItems(true));
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            holdTime = 0;


        if (Input.GetKeyDown(KeyCode.A))
        {
            if (curSongDif != 0)
            {
                m_DifImgs[curSongDif].sprite = m_DifUnSelectSprites[curSongDif];
                curSongDif--;
                m_DifImgs[curSongDif].sprite = m_DifSelectSprites[curSongDif];
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (curSongDif != 3)
            {
                m_DifImgs[curSongDif].sprite = m_DifUnSelectSprites[curSongDif];
                curSongDif++;
                m_DifImgs[curSongDif].sprite = m_DifSelectSprites[curSongDif];
            }
        }
    }

    public void Initialize()
    {
        for(int i = 0; i < 9; i++)
        {
            m_MusicItems[i] = CreateItem(GetNum(i - 4));
            m_MusicItems[i].transform.localPosition += new Vector3(0, -70 * i, 0);
        }

        m_MusicItems[4].Selected();

        ChangeSongInfo();
    }

    void ChangeSongInfo()
    {
        MusicInfo musicInfo = MusicInfo.Inst;

        m_SongNameTxt.text = musicInfo.songNames[curSongNum];
        m_ComposserTxt.text = musicInfo.compossers[curSongNum];
        m_BPMTxt.text = string.Format("BPM : {0}", musicInfo.songBPMs[curSongNum]);
    }

    IEnumerator IEnum_MoveMusicItems(bool isDown)
    {
        canMove = false;
        if (isDown)
        {
            m_MusicItems[4].UnSelected();
            m_MusicItems[5].Selected();
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < m_MusicItems.Length; j++)
                {
                    m_MusicItems[j].transform.localPosition += new Vector3(0, 7, 0);
                }
                yield return new WaitForSeconds(0.01f);
            }

            Destroy(m_MusicItems[0]);
            for(int i = 0; i < m_MusicItems.Length - 1; i++)
            {
                m_MusicItems[i] = m_MusicItems[i + 1];
            }
            curSongNum = GetNum(curSongNum + 1);
            m_MusicItems[8] = CreateItem(GetNum(curSongNum + 4));
            m_MusicItems[8].transform.localPosition += new Vector3(0, -560, 0);
        }
        else if(!isDown)
        {
            m_MusicItems[4].UnSelected();
            m_MusicItems[3].Selected();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < m_MusicItems.Length; j++)
                {
                    m_MusicItems[j].transform.localPosition += new Vector3(0, -7, 0);
                }
                yield return new WaitForSeconds(0.01f);
            }

            Destroy(m_MusicItems[8]);
            for (int i = m_MusicItems.Length - 1; i > 0; i--)
            {
                m_MusicItems[i] = m_MusicItems[i - 1];
            }
            curSongNum = GetNum(curSongNum - 1);
            m_MusicItems[0] = CreateItem(GetNum(curSongNum - 4));
        }
        ChangeSongInfo(); 
        StartCoroutine(IEnum_ChangeSongImg());
        canMove = true;
    }

    MusicItem CreateItem(int num)
    {
        GameObject go = Instantiate(m_MusicInfoPref, m_MusingInfoParent);
        MusicItem kItem = go.GetComponent<MusicItem>();
        kItem.Initialize(GetNum(num));

        return kItem;
    }

    int GetNum(int n)
    {
        int num = n;
        int musicLength = MusicInfo.Inst.musicLength;

        while(num < 0)
        {
            num += musicLength;
        }

        while(num >= musicLength)
        {
            num -= musicLength;
        }

        return num;
    }

    IEnumerator IEnum_ChangeSongImg()
    {
        for(int i = 0; i < 10; i++)
        {
            m_curSongImg.transform.Rotate(new Vector3(9, 0, 0));
            float y = Mathf.Lerp(m_curSongImg.transform.localPosition.y, 260, 0.05f);
            m_curSongImg.transform.localPosition = new Vector3(0, y, 0);
            yield return new WaitForSeconds(0.006f);
        }

        m_curSongImg.sprite = MusicInfo.Inst.songImgs[curSongNum];
        m_curSongImg.transform.localPosition = new Vector3(0, 20, 0);

        for (int i = 0; i < 10; i++)
        {
            m_curSongImg.transform.Rotate(new Vector3(-9, 0, 0));
            m_curSongImg.transform.localPosition += new Vector3(0, 12f, 0);
            yield return new WaitForSeconds(0.006f);
        }
    }
}
