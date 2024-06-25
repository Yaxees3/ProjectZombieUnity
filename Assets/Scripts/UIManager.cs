using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIMenager : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText;
    public Image healthBarFilll;

    [Header("Paused Menu")]
    public GameObject pauseMenu;

    [Header("End Game Screen")]
    public GameObject endGameScreen;
    public TextMeshProUGUI endGameHeaderText;
    public TextMeshProUGUI endGameScoreText;


    public static UIMenager instance;

    private void Awake()
    {
        instance = this; 
    }

    public void UpdateHEalthBar(int currentHP, int maxHP)
    {
        healthBarFilll.fillAmount = (float)currentHP/(float)maxHP;
    }

    public void  UpdateScoreText(int score)
    {
        scoreText.text = "Score" + score; 
    }

    public void UpdateAmmoText(int currentAmmo, int maxAmmo)
    {
        ammoText.text = "Ammo: " + currentAmmo + " / " + maxAmmo;
    }

    public void TogglePauseMenu(bool paused)
    {
        pauseMenu.SetActive(paused);
    }

    public void SetEndGameScreen(bool won, int score)
    {
        endGameScreen.SetActive(true);
        endGameHeaderText.text = won == true ? "Wygranko" : "Przegranko";
        endGameHeaderText.color = won == true ? Color.green : Color.red;
        endGameScoreText.text = "<b>Score</b>" + score;
  
    }

    public void OnResumeButron()
    {

    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene("Game");

    }
    public void OnMenuButton()
    {
        SceneManager.LoadScene("Menu");

    }
}
