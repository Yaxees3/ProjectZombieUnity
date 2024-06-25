using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int scoreToWin;
    public int currentScore;

    public bool gamePaused;

    public static GameManager instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            TogglePausedGame();
    }

    public void TogglePausedGame()
    {
        gamePaused = !gamePaused;

        Time.timeScale = gamePaused == true ? 0.0f : 1.0f;

        Cursor.lockState = gamePaused == true ? CursorLockMode.None : CursorLockMode.Locked;

        UIMenager.instance.TogglePauseMenu(gamePaused);
    }

    public void AddScore(int score)
    {
        currentScore += score;

        UIMenager.instance.UpdateScoreText(currentScore);

        if (currentScore >= scoreToWin)
            WinGame();
    }
    
    void WinGame()
    {
        UIMenager.instance.SetEndGameScreen(true, currentScore);
        Time.timeScale = 0.0f;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void LoseGame()
    {
        UIMenager.instance.SetEndGameScreen(false, currentScore);
        Time.timeScale = 0.0f;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
