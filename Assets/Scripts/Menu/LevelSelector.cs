using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    private GameManager gm;

    public Button[] levelButtons;

    void Awake()
    {
        gm = GameManager.instance;
    }

    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            var index = i;
            levelButtons[i].onClick.AddListener(delegate { gm.LoadLevel(index); });
        }
    }

    public void updateLevels()
    {
        int levelReached = gm.levelReached;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].GetComponent<Image>().color = Color.gray;
                levelButtons[i].interactable = false;
            }
        }
    }
}
