using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLoadingPanel : PanelHide
{
    private GameManager gm;

    public int dayToload;

    void Awake()
    {
        gm = GameManager.instance;
    }

    public void loadDay()
    {
        gm.loadDay(dayToload);
        gameObject.GetComponent<LevelSelector>().updateLevels();
    }
}
