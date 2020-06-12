using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public GameObject newRecordText;

	public int playScore;
	public int recordScore; //haetaan PlayerPrefs:sista nimellä HighScore (int)

	public Text currentText;
	public Text recordText;

	void Start () {
		playScore = 0;
		recordScore = PlayerPrefs.GetInt("HighScore", 0);
		recordText.text = recordScore.ToString();
		newRecordText.SetActive(false);
	}
	
	void Update () {
		currentText.text = playScore.ToString();
	}

    //Tallentaa pisteet
	public void SaveHighScore () {
		if(playScore > PlayerPrefs.GetInt("HighScore", 0)){
			newRecordText.SetActive(true);
			PlayerPrefs.SetInt("HighScore", playScore);
		}
	}

	//Saa pisteet hirviön voittamisesta
	public void AddMonsterKillPoints () {
		playScore += 100;
	}

	//Saa pisteet hirviön tappamisesta Kultaisella Power Upilla
	public void AddBoostedMonsterKillPoints () {
		playScore += 150;
	}

	//Saa pisteet bossin tappamisesta
	public void AddBossMonsterKillPoints () {
		playScore += 500;
	}

	//Saa pisteet tavaran poimimisesta
	public void AddItemPoints () {
		playScore += 20;
	}
}
