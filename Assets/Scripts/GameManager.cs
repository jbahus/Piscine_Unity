using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    private int[] d01Levels = new int[] { 1, 2 };
    public int[] currentLevels;

    public int levelIndex;
    public int levelReached;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextLevel()
    {
        levelIndex++;
    }

    public void loadDay(int dayNum)
    {
        switch (dayNum)
        {
            case 1:
                levelReached = PlayerPrefs.GetInt("levelReachedD01", 1);
                currentLevels = d01Levels;
                break;
        }
        
    }

    public void LoadLevel(int i)
    {
        levelIndex = i;
        SceneManager.LoadScene(d01Levels[levelIndex]);
    }

    public void winLevel(int dayNum, int levelNum)
    {
        switch (dayNum)
        {
            case 1:
                PlayerPrefs.SetInt("levelReachedD01", levelNum);
                break;
        }
    }

    public void deleteSaveByDay(int dayNum)
    {
        switch (dayNum)
        {
            case 1:
                PlayerPrefs.DeleteKey("levelReachedD01");
                break;
            default:
                break;
        }
    }
}
