using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHide : MonoBehaviour
{
    public void hide()
    {
        gameObject.SetActive(false);
    }

    public void show()
    {
        gameObject.SetActive(true);
    }
}
