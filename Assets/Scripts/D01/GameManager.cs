using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    private int[] d01Levels = new int[] { 0, 1 };
    public int[] currentLevels;

    public int levelIndex;

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
        currentLevels = d01Levels;
        levelIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextLevel()
    {
        levelIndex++;
    }
}
