using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePlayerCollision : MonoBehaviour {
    //Kaikille vihollisille, antaa pelaajan mennä vihollisten lapi

    //public GameObject pelaaja;
    GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
    }


    void Update() {
        
    }
}
