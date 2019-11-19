using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    GameManager instance;

    private bool redExit;
    private bool yellowExit;
    private bool blueExit;

    private GameObject red;
    private GameObject yellow;
    private GameObject blue;

    public GameObject playersSpawnPoint;
    public GameObject exitsSpawnPoint;

    public GameObject playersInstance;
    public GameObject exitsInstance;

    public GameObject ManageCamera;

    private Animator animator;

    private int levelToLoad;
    private bool onetimeOnly;

    // Start is called before the first frame update
    void Start()
    {
        instance = GameManager.instance;
        animator = GetComponent<Animator>();
        onetimeOnly = true;

        GameObject players = Instantiate(playersInstance);
        GameObject exits = Instantiate(exitsInstance);

        red = players.transform.Find("red").gameObject;
        yellow = players.transform.Find("yellow").gameObject;
        blue = players.transform.Find("blue").gameObject;

        players.transform.position = playersSpawnPoint.transform.position;
        exits.transform.position = exitsSpawnPoint.transform.position;

        ManageCamera.GetComponent<ManageCamera>().setupCamera(red, yellow, blue);

        ExitLevel exitR = exits.transform.Find("red_exit").gameObject.GetComponent<ExitLevel>();
        ExitLevel exitY = exits.transform.Find("yellow_exit").gameObject.GetComponent<ExitLevel>();
        ExitLevel exitB = exits.transform.Find("blue_exit").gameObject.GetComponent<ExitLevel>();

        exitR.setPlayerName(red.name);
        exitR.setLevelManager(this);
        exitY.setPlayerName(yellow.name);
        exitY.setLevelManager(this);
        exitB.setPlayerName(blue.name);
        exitB.setLevelManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (redExit && yellowExit && blueExit && onetimeOnly)
        {
            onetimeOnly = false;
            fadeToLevel();
            instance.nextLevel();
            levelToLoad = instance.currentLevels[instance.levelIndex];
        }
    }

    private void fadeToLevel()
    {
        animator.SetTrigger("Fade");
    }

    public void onFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void exitHandler(string name, bool state)
    {
        switch (name)
        {
            case "red":
                redExit = state;
                break;
            case "yellow":
                yellowExit = state;
                break;
            case "blue":
                blueExit = state;
                break;
        }
    }
}
