using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour {

    //Komponentit
	AudioSource audio;
	//public GameObject[] enemies;
	//public GameObject[] bosses;
	public EnemySpawner spawnerA;
	public EnemySpawner spawnerB;
	public EnemySpawner spawnerC;
	public EnemySpawner spawnerD;
	public Text roundText;
	EndGame endGame;

	public int rounds;
	int roundsForText;
	public int numberOfEnemies;
	//public int numberOfBosses;
    public bool saaVaihtaaRoundin;

	//Boss spawn
	public GameObject boss;
	public int bossCount;

	public int bossCounter;

	public bool playerWins;

	void Start () {
		bossCounter = 0;
		rounds = 0;
		roundText.text = rounds.ToString();
		bossCount = 0;
		playerWins = false;
		audio = GetComponent<AudioSource>();
        saaVaihtaaRoundin = false;
		//enemies = GameObject.FindGameObjectsWithTag("EnemySpawner");
		endGame = gameObject.GetComponent<EndGame>();
	}
	
	void Update () {
		//Cheat codet (poista valmiista versiosta)
		/*if(bossCounter > 0){
			bossCounter--;
		}
		if(Input.GetKey(KeyCode.B) && bossCounter <= 0){
			BossLevelStart();
			bossCounter = 100;
		}
		else if(Input.GetKey(KeyCode.V) && bossCounter <= 0){
			VaihdaRoundiHeti();
			bossCounter = 100;
		}
		*/
		
		//enemies = GameObject.FindGameObjectsWithTag("Hitbox");
		//bosses = GameObject.FindGameObjectsWithTag("Boss");


        //Aloittaa uuden roundin
		if(numberOfEnemies <= 0 && saaVaihtaaRoundin == true){
			rounds++;
			roundText.text = rounds.ToString();
			audio.Play();

			/* 
			//SIISTI TÄMÄ; TURHAAN MONTA IFFIÄ
			if(rounds == 5 || rounds == 10){
				BossLevelStart();
			}
			else if(rounds == 10 || rounds == 15){
				BossLevelStart();
			}
			else if(rounds == 20 || rounds == 25){
				BossLevelStart();
			}
			else if(rounds == 30 || rounds == 35){
				BossLevelStart();
			}
			else if(rounds == 40 || rounds == 45){
				BossLevelStart();
			}
			else{
				spawnerA.UusiKierrosAlkaa();
				spawnerB.UusiKierrosAlkaa();
				spawnerC.UusiKierrosAlkaa();
				spawnerD.UusiKierrosAlkaa();
			}
			*/

			switch (rounds){
				case 5:
					BossLevelStart();
					break;
				case 10:
					BossLevelStart();
					break;
				case 11:
					playerWins = true;
					endGame.Victory();
					break;
				default:
					spawnerA.UusiKierrosAlkaa();
					spawnerB.UusiKierrosAlkaa();
					spawnerC.UusiKierrosAlkaa();
					spawnerD.UusiKierrosAlkaa();
					break;
			}

		}

        if (numberOfEnemies != null){
            saaVaihtaaRoundin = true;
        }
    }

	void BossLevelStart () {
		//spawnaa bosseja sen mukaan monesko bossitaso
		//bossCount++;
		Instantiate(boss, new Vector3(0F, 0F, -1.5F), Quaternion.identity);
	}

	void VaihdaRoundiHeti () {
		rounds++;
		roundText.text = rounds.ToString();
		audio.Play();

		switch (rounds){
			case 5:
				BossLevelStart();
				break;

			case 10:
				BossLevelStart();
				break;

			case 11:
				endGame.Victory();
				break;

			default:
				spawnerA.UusiKierrosAlkaa();
				spawnerB.UusiKierrosAlkaa();
				spawnerC.UusiKierrosAlkaa();
				spawnerD.UusiKierrosAlkaa();
				break;
		}
	}

}
