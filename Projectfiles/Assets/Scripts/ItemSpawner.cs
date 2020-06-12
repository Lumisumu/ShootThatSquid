using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
	
    //Komponentit
	public GameObject vihreaTynnyri;
	public GameObject sininenTynnyri;
	public GameObject keltainenTynnyri;

	bool lupaSpawnata;
	bool ekaSpawnattu;
	bool spawningStarted;
	int arvottuLuku;

	void Start () {
		lupaSpawnata = true;
		ekaSpawnattu = false;
		spawningStarted = false;
	}
	
	void Update () {

		//Spawnaus
		if(lupaSpawnata == true){

			//Spawnaa normaalisti eli 15 sek viiveella
			if(ekaSpawnattu == true){
				if(spawningStarted == false){
					spawningStarted = true;
					StartCoroutine(SpawnaaViiveella(10.0f)); //sulkuihin spawnausaika
				}
			}

			//Kierroksen eka spawn tapahtuu heti
			else{
					ArvoTynnyri();
					SpawnaaUusiTynnyri();
					ekaSpawnattu = true;
					lupaSpawnata = false;
			}
		}
	}

	//Arvotaan tynnyri
	private void ArvoTynnyri () {
		arvottuLuku = Random.Range(0, 5);
	}

    //Spawnataan tynnyri arvotun luvun perusteella
	private void SpawnaaUusiTynnyri () {
		switch(arvottuLuku){
			case 0:
				Instantiate(keltainenTynnyri, transform.position, Quaternion.identity);
				break;
			case 1:
				Instantiate(vihreaTynnyri, transform.position, Quaternion.identity);
				break;
			case 2:
				Instantiate(vihreaTynnyri, transform.position, Quaternion.identity);
				break;
			case 3:
				Instantiate(sininenTynnyri, transform.position, Quaternion.identity);
				break;
			case 4:
				Instantiate(sininenTynnyri, transform.position, Quaternion.identity);
				break;
			case 5:
				Instantiate(sininenTynnyri, transform.position, Quaternion.identity);
				break;
			default:
				Instantiate(sininenTynnyri, transform.position, Quaternion.identity);
				break;
		}

		/*
		if(arvottuLuku == 0 || arvottuLuku == ){
			Instantiate(keltainenTynnyri, transform.position, Quaternion.identity);
		}
		else if(arvottuLuku == 1){
			Instantiate(sininenTynnyri, transform.position, Quaternion.identity);
		}
		else{
			Instantiate(vihreaTynnyri, transform.position, Quaternion.identity);
		}
		*/
	}

	//Kaytetaan jos halutaan asettaa spawnaamispaikka manuaalisesti ylla olevassa Instantiessa
	Vector3 GeneratedPosition () {
		float x, y, z;
		x = transform.position.x;
		y = -1.5f;
		z = transform.position.z;

		return new Vector3(x, y, z);
	}

	private IEnumerator SpawnaaViiveella (float delay) {
		yield return new WaitForSecondsRealtime(delay);
		ArvoTynnyri();
		SpawnaaUusiTynnyri();
		lupaSpawnata = false;
		spawningStarted = false;
		yield return 0;
	}

    //Alkaa spawnata uutta kun pelaaja poimii tavaran
	void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Player"){
			lupaSpawnata = true;
		}
	}
}
