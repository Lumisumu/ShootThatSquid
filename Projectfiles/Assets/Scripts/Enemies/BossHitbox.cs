using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitbox : MonoBehaviour {
    
    //Komponentit
    GameObject player;
    GameObject ship;
    ItemManager itemManager;
	ScoreManager scoreManager;
    GameObject roundSystem;
	RoundManager roundManager;
    
    public int enemyHealth;

	void Start () {
        player = GameObject.FindGameObjectWithTag("ScoreManager");
	    scoreManager = player.GetComponent<ScoreManager>();

        roundSystem = GameObject.FindGameObjectWithTag("RoundSystem");
		roundManager = roundSystem.GetComponent<RoundManager>();

        ship = GameObject.FindGameObjectWithTag("Player");
        itemManager = ship.GetComponent<ItemManager>();

        roundManager.numberOfEnemies++;

        if(roundManager.rounds == 5){
            enemyHealth = 10;
        }
        else if(roundManager.rounds == 10){
            enemyHealth = 15;
        }
    }

    void Update () {

        //Kuolee helan loppuessa
        if(enemyHealth <= 0){
            itemManager.PlayHurtSound();
            roundManager.numberOfEnemies--;
            scoreManager.AddBossMonsterKillPoints();
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
