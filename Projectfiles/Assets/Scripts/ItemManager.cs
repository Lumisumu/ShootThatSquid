using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    //Komponentit
    BoatAlignNormal boatAlignNormal;
	PlayerHealth playerHealth;
	GameObject player;
	ScoreManager scoreManager;
    AudioSource audio;
	public ParticleSystem blueBling;
	public ParticleSystem goldBling;
	public ParticleSystem blueSmoke;
	public ParticleSystem goldSmoke;

    //Pelaajan powerupit
    public int speedBoostCount;
    public int destroyerBoostCount;
    public GameObject[] speed;
    public GameObject[] destroyer;

    //Poweruppien kaytto
    public bool speedPowered;
    public bool destroyerPowered;
    int speedTimer;
    public int destroyerTimer;

	//Vihollisen aani
	public AudioClip hurtSound;


	void Start () {
		speed[0].SetActive(false);
		speed[1].SetActive(false);

		destroyer[0].SetActive(false);
		destroyer[1].SetActive(false);

		speedPowered = false;
		destroyerPowered = false;

		boatAlignNormal = GetComponent<BoatAlignNormal>();
		playerHealth = GetComponent<PlayerHealth>();

		player = GameObject.FindGameObjectWithTag("ScoreManager");
		scoreManager = player.GetComponent<ScoreManager>();

		audio = GetComponent<AudioSource>();
	}

	void Update () {
        UpdateSpeedBoostCounter();
        UpdateDestroyerBoostCounter();

        //Powerup paalla
        if (speedPowered == true){
			speedTimer--;

			if(speedTimer <= 0){
				boatAlignNormal._enginePower = 11;
				speedPowered = false;
			}
		}

		if(destroyerPowered == true){
			destroyerTimer--;

			if(destroyerTimer <= 0){
				playerHealth.invincibilityMode = false;
				destroyerPowered = false;
			}
		}
        
        //Inputit
		if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.JoystickButton2)){
            if(speedPowered == false && speedBoostCount > 0)
            {
                UseSpeedBoost();
            }
        }
        
		if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.JoystickButton3)){
            if (destroyerPowered == false && destroyerBoostCount > 0)
            {
                UseDestroyerBoost();
            }
        }
    }
	
	//Tunnistetaan kerättävä power up
	void OnTriggerEnter(Collider other) {
		//Sininen tynnyri kerataan
		if(other.CompareTag("Crate")){
			speedBoostCount += 1;
			UpdateSpeedBoostCounter();
			scoreManager.AddItemPoints();
			PlayItemPickSound();
			blueBling.Play();
			Destroy(other.gameObject);
		}

		//Keltainen tynnyri kerataan
		else if(other.CompareTag("Chest")){
			destroyerBoostCount += 1;
			UpdateDestroyerBoostCounter();
			scoreManager.AddItemPoints();
			PlayItemPickSound();
			goldBling.Play();
			Destroy(other.gameObject);
		}

        //Vihrea tynnyri kerataan
		else if(other.CompareTag("Barrel")){
			PlayItemPickSound();
		}
	}

	public void PlayHurtSound () {
		audio.PlayOneShot (hurtSound, 0.25f);
	}

	//Käytetään speed power up
	void UseSpeedBoost () {
		speedPowered = true;
		boatAlignNormal._enginePower = 25;
		speedTimer = 400;
		speedBoostCount--;
		blueSmoke.Play();
		UpdateSpeedBoostCounter();
	}

	//Käytetään attack power up
	void UseDestroyerBoost () {
		destroyerPowered = true;
		playerHealth.invincibilityMode = true;
		destroyerTimer = 400;
		destroyerBoostCount--;
		goldSmoke.Play();
		UpdateDestroyerBoostCounter();
	}

	void UpdateSpeedBoostCounter () {
		if(speedBoostCount > 2){
			speedBoostCount = 2;
		}

		if(speedBoostCount <= 0){
			speed[0].SetActive(false);
			speed[1].SetActive(false);
		}
		else if(speedBoostCount == 1){
			speed[0].SetActive(true);
			speed[1].SetActive(false);
		}
		else if(speedBoostCount == 2){
			speed[0].SetActive(true);
			speed[1].SetActive(true);
		}
	}

	void UpdateDestroyerBoostCounter () {
		if(destroyerBoostCount > 2){
			destroyerBoostCount = 2;
		}

		if(destroyerBoostCount <= 0){
			destroyer[0].SetActive(false);
			destroyer[1].SetActive(false);
		}
		else if(destroyerBoostCount == 1){
			destroyer[0].SetActive(true);
			destroyer[1].SetActive(false);
		}
		else if(destroyerBoostCount == 2){
			destroyer[0].SetActive(true);
			destroyer[1].SetActive(true);
		}
	}

    //Aani kun tavara kerataan
	public void PlayItemPickSound () {
		audio.Play(0);
	}
}
