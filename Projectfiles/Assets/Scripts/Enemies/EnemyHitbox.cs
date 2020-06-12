using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour {
    
    //Komponentit
    GameObject player;
    GameObject ship;
    ItemManager itemManager;
	ScoreManager scoreManager;
    GameObject roundSystem;
	RoundManager roundManager;

    int rounds;
    
    public int enemyHealth;

	void Start () {
        player = GameObject.FindGameObjectWithTag("ScoreManager");
	    scoreManager = player.GetComponent<ScoreManager>();

        ship = GameObject.FindGameObjectWithTag("Player");
        itemManager = ship.GetComponent<ItemManager>();

        roundSystem = GameObject.FindGameObjectWithTag("RoundSystem");
		roundManager = roundSystem.GetComponent<RoundManager>();

        roundManager.numberOfEnemies++;
        rounds = roundManager.rounds;

        switch(rounds){
            case 6:
                enemyHealth = 5;
                break;
            case 7:
                enemyHealth = 6;
                break;
            case 8:
                enemyHealth = 7;
                break;
            case 9:
                enemyHealth = 8;
                break;
            default:
                enemyHealth = 3;
                break;
        }
    }

    void Update () {

        //Kuolee helan loppuessa
        if(enemyHealth <= 0){
            itemManager.PlayHurtSound();
            roundManager.numberOfEnemies--;
            scoreManager.AddMonsterKillPoints();
            Destroy(gameObject);
        }
	}

    //Kanuunankuulan osuessa
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            itemManager.PlayHurtSound();
            enemyHealth -= 1;
            Destroy(other.gameObject);
        }
    }
}
