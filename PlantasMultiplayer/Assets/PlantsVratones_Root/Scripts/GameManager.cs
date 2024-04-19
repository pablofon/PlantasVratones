using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("GameManager is null!");
            }
            return instance;
        }
    }

    [Header("Game Status")]
    public bool gameCompleted = false;
    public bool gameOver = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        gameCompleted = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            GameOver();
        }
    }

    public void GameReset()
    {
        gameCompleted = false;
        gameOver = false;
    }

    void GameOver()
    {
        if (gameOver)
        {
            Debug.Log("Has perdido");
        }
    }
}
