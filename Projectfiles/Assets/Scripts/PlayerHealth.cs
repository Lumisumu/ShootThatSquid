using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    //Komponentit
	GameObject scoreMang;
	ScoreManager scoreManager;
	ItemManager itemManager;
	AudioSource audio;

    //Roundi systeemi
	GameObject roundSystem;
	RoundManager roundManager;
	EndGame endGame;

    //UI
	public AudioClip gameOverSound;
	public GameObject[] heart;
	public GameObject[] skull;
	public GameObject gameOverTeksti;

    //Particle effectit
	public ParticleSystem smoke;
	public ParticleSystem goldBling;
	public ParticleSystem greenBling;

    public int health;
    public bool invincibilityMode;
    public int invincibilityTime;

	//Bossin skripti
	public BossHitbox bossHitbox;

    void Start () {
		scoreMang = GameObject.FindGameObjectWithTag("ScoreManager");
		scoreManager = scoreMang.GetComponent<ScoreManager>();
		itemManager = GetComponent<ItemManager>();
		audio = GetComponent<AudioSource>();

		Time.timeScale = 1f;
		gameOverTeksti.SetActive(false);
		ShowHearts();
		invincibilityMode = false;

		roundSystem = GameObject.FindGameObjectWithTag("RoundSystem");
		roundManager = roundSystem.GetComponent<RoundManager>();
		endGame = roundSystem.GetComponent<EndGame>();

        health = 3;
	}
	
	void Update () {
		//Tasaa healthin jos menee yli 3:n
		if(health > 3){
			health = 3;
		}

        //Paivittaa UIn
		UpdateHearts();

        //Kuolee
		if(health <= 0 && roundManager.playerWins != true){
			Debug.Log("KUALIT!");
			/*
			gameOverTeksti.SetActive(true);
			scoreManager.SaveHighScore();
			Time.timeScale = 0.25f;
			StartCoroutine(GOwait());
			*/
			endGame.GameOver();
		}

        //Kuolemattomuus
		if(invincibilityMode == true){
			invincibilityTime--;

			if(invincibilityTime <= 0){
				invincibilityMode = false;
				invincibilityTime = 0;
			}
		}
	}

	IEnumerator GOwait () {
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("MainMenu");
	}

    //Osuma viholliseen
	void OnTriggerStay(Collider other) {
		if(other.CompareTag("Hitbox") || other.CompareTag("Boss")){

			//Törmää viholliseen tai bossiin
			if(invincibilityMode == false && itemManager.destroyerPowered == false){
				health -= 1;
				invincibilityMode = true;
				invincibilityTime = 250;
				audio.PlayOneShot (gameOverSound, 1.0f);
				smoke.Play();
			}

			//Törmää viholliseen boostin kanssa
			if(itemManager.destroyerPowered == true && other.CompareTag("Hitbox")){
				Debug.Log("OSUI VIHOLLISEEN BOOSTIN KANSSA!");
				roundManager.numberOfEnemies--;
				scoreManager.AddBoostedMonsterKillPoints();
				Destroy(other.transform.parent.gameObject);
			}
		}
	}

    //Ottaa vihrean tynnyrin
	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Barrel")){
			health += 1;
			scoreManager.AddItemPoints();
			greenBling.Play();
			Destroy(other.gameObject);
		}
	}

    //UI
	void ShowHearts () {
		skull[0].SetActive(false);
		skull[1].SetActive(false);
		skull[2].SetActive(false);

		heart[0].SetActive(true);
		heart[1].SetActive(true);
		heart[2].SetActive(true);
	}

	void ShowSkulls () {
		heart[0].SetActive(false);
		heart[1].SetActive(false);
		heart[2].SetActive(false);

		skull[0].SetActive(true);
		skull[1].SetActive(true);
		skull[2].SetActive(true);
	}

	void UpdateHearts () {
		if(health == 3){
			ShowHearts();
		}
		else if(health == 2){
			skull[0].SetActive(false);
			skull[1].SetActive(false);
			skull[2].SetActive(true);

			heart[0].SetActive(true);
			heart[1].SetActive(true);
			heart[2].SetActive(false);
		}
		else if(health == 1){
			skull[0].SetActive(false);
			skull[1].SetActive(true);
			skull[2].SetActive(true);

			heart[0].SetActive(true);
			heart[1].SetActive(false);
			heart[2].SetActive(false);
		}
		else{
			ShowSkulls();
		}
	}
}
