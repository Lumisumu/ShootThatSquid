using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	
    //Komponentit
	public GameObject enemy;
	GameObject roundSystem;
	RoundManager roundManager;

	int vertaaRounds;
	public int roundCount;
	int numberOfSpawns;

	int arvottuLuku;

	void Start () {
		roundCount = 0;
		numberOfSpawns = roundCount;

		roundSystem = GameObject.FindGameObjectWithTag("RoundSystem");
		roundManager = roundSystem.GetComponent<RoundManager>();

		UusiKierrosAlkaa();
	}
	
	public void UusiKierrosAlkaa () {
		roundCount = roundManager.rounds;
		numberOfSpawns = roundCount;
		/*
		if(roundCount != vertaaRounds){
			roundCount = vertaaRounds;
			numberOfSpawns = roundCount;
		}

		//Aloittaa uuden spawnauskierroksen
		if(numberOfSpawns > 0){
			SpawnaaUusiVihollinen();
		}
		*/

		//Debug.Log("NEW LEVEL BEGIN !!!");
		
		if(numberOfSpawns > 4){
			numberOfSpawns = 4;
		}

		//Aloittaa uuden spawnauskierroksen, spawnaa roundien määrän mukaan
		while(numberOfSpawns > 0 && numberOfSpawns < 5){
			Instantiate(enemy, GeneratedPosition(), Quaternion.identity);
			numberOfSpawns--;
		}
	}

	//Hajottaa hiukan spawnaavaa vihollisjoukkoa
	Vector3 GeneratedPosition () {
		float x, y, z;
		arvottuLuku = Random.Range(0, 4);

		if(arvottuLuku == 0){
			x = transform.position.x + (numberOfSpawns * 6);
			y = -1.5f;
			z = transform.position.z + (numberOfSpawns * 6);
		}
		else if(arvottuLuku == 1){
			x = transform.position.x - (numberOfSpawns * 6);
			y = -1.5f;
			z = transform.position.z - (numberOfSpawns * 6);
		}
		else if(arvottuLuku == 2){
			x = transform.position.x - (numberOfSpawns * 6);
			y = -1.5f;
			z = transform.position.z + (numberOfSpawns * 6);
		}
		else{
			x = transform.position.x + (numberOfSpawns * 6);
			y = -1.5f;
			z = transform.position.z - (numberOfSpawns * 6);
		}

		return new Vector3(x, y, z);
	}
}
