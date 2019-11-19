using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    private string playerName;
    LevelManager levelManager;

    public void setPlayerName(string name)
    {
        playerName = name;
    }

    public void setLevelManager(LevelManager levelManager)
    {
        this.levelManager = levelManager;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject tmp = collision.gameObject;
        if (tmp.name == playerName &&
            tmp.transform.position.x > transform.position.x - 0.1 &&
            tmp.transform.position.x < transform.position.x + 0.1 &&
            tmp.transform.position.y > transform.position.y - 0.1 &&
            tmp.transform.position.y < transform.position.y + 0.1)
        {
           levelManager.exitHandler(tmp.name, true);
        }
        else
        {
           levelManager.exitHandler(tmp.name, false);
        }
    }
}
