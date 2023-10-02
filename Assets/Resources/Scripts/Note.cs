using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_SpriteRender = null;
    public int curIdx = -1;

    public void SetNote(int i)
    {
        if (curIdx == i)
        {
            m_SpriteRender.color = new Color(0, 0, 0, 0);
            curIdx = -1;
            return;
        }

        switch (i)
        {
            case 0:
                {
                    m_SpriteRender.color = new Color(1, 1, 1, 1);
                    curIdx = 0;
                    break;
                }
        }
    }
}
