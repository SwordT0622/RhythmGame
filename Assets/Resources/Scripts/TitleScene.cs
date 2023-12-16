using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] Sprite[] m_SelectSprite = null;
    [SerializeField] SpriteRenderer[] m_Menus = null;
    [SerializeField] OptionDlg m_OptionDlg = null;
    int curMenu = 0;

    private void Start()
    {
        m_OptionDlg.gameObject.SetActive(false);
        curMenu = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (curMenu == 0)
                return;

            m_Menus[curMenu].sprite = m_SelectSprite[0];
            curMenu--;
            m_Menus[curMenu].sprite = m_SelectSprite[1];
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (curMenu == m_Menus.Length - 1)
                return;

            m_Menus[curMenu].sprite = m_SelectSprite[0];
            curMenu++;
            m_Menus[curMenu].sprite = m_SelectSprite[1];
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (curMenu)
            {
                case 0:
                    {
                        SceneManager.LoadScene("SelectScene");
                        break;
                    }
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        m_OptionDlg.gameObject.SetActive(true);
                        break;
                    }
                case 3:
                    {
                        Application.Quit();
                        break;
                    }
            }
        }
    }
}
