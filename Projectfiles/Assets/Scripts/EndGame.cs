using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    GameObject player;
	ScoreManager scoreManager;
    public GameObject gameUI;
    public GameObject gameOverTeksti;
    public GameObject winTeksti;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("ScoreManager");
	    scoreManager = player.GetComponent<ScoreManager>();

        gameUI.SetActive(true);
        gameOverTeksti.SetActive(false);
        winTeksti.SetActive(false);
    }

    public void GameOver () {
        gameUI.SetActive(false);
        gameOverTeksti.SetActive(true);
		SaveResults();
    }

    public void Victory () {
        gameUI.SetActive(false);
        winTeksti.SetActive(true);
		SaveResults();
    }

    public void SaveResults () {
        scoreManager.SaveHighScore();
		Time.timeScale = 0.25f;
		StartCoroutine(GOwait());
    }

    IEnumerator GOwait () {
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("MainMenu");
	}
}
