using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    GameManager instance;

    public bool redInExit;
    public bool yellowInExit;
    public bool blueInExit;

    public GameObject redPlayer;
    public GameObject yellowPlayer;
    public GameObject bluePlayer;

    public GameObject redExit;
    public GameObject yellowExit;
    public GameObject blueExit;

    public GameObject ManageCamera;

    private Animator animator;

    private int levelToLoad;
    private bool onetimeOnly;

    public int levelNum;
    public int dayNum;

    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        instance = GameManager.instance;
        animator = GetComponent<Animator>();
        onetimeOnly = true;

        ManageCamera.GetComponent<ManageCamera>().setupCamera(redPlayer, yellowPlayer, bluePlayer);

        ExitLevel exitR = redExit.GetComponent<ExitLevel>();
        ExitLevel exitY = yellowExit.GetComponent<ExitLevel>();
        ExitLevel exitB = blueExit.gameObject.GetComponent<ExitLevel>();

        exitR.setPlayerName(redPlayer.name);
        exitR.setLevelManager(this);
        exitY.setPlayerName(yellowPlayer.name);
        exitY.setLevelManager(this);
        exitB.setPlayerName(bluePlayer.name);
        exitB.setLevelManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (redInExit && yellowInExit && blueInExit && onetimeOnly)
        {
            onetimeOnly = false;
            fadeToLevel();
            instance.nextLevel();
            instance.winLevel(dayNum, levelNum);
            levelToLoad = instance.currentLevels[instance.levelIndex];
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !pausePanel.activeSelf)
        {
            pausePanel.GetComponent<PanelHide>().show();
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pausePanel.activeSelf)
        {
            pausePanel.GetComponent<PanelHide>().hide();
            Time.timeScale = 1;
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
                redInExit = state;
                break;
            case "yellow":
                yellowInExit = state;
                break;
            case "blue":
                blueInExit = state;
                break;
        }
    }

    public void loadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
