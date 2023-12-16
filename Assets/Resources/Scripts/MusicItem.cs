using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MusicItem : MonoBehaviour
{
    [SerializeField] Slider m_SelectBar = null;
    [SerializeField] Image m_SongImg = null;
    [SerializeField] Text m_SongName = null;

    public void Initialize(int num)
    {
        m_SongImg.sprite = MusicInfo.Inst.songImgs[num];
        m_SongName.text = MusicInfo.Inst.songNames[num];
    }

    public void Selected()
    {
        StopAllCoroutines();
        StartCoroutine(IEnum_ChangeValue(0.6f, true));
    }

    public void UnSelected()
    {
        StopAllCoroutines();
        StartCoroutine(IEnum_ChangeValue(0.6f, false));
    }

    IEnumerator IEnum_ChangeValue(float time, bool isUp)
    {
        float t = 0;
        while (t < time)
        {
            float value;
            if (isUp)
                value = Mathf.Lerp(m_SelectBar.value, 1, t / time);
            else
                value = Mathf.Lerp(m_SelectBar.value, 0, t / time);

            m_SelectBar.value = value;
            t += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
