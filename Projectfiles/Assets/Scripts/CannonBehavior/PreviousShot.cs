using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousShot : MonoBehaviour {

    //Tata skriptia kaytetan viimeisen laukaisun visualisointiin, jotta se helpottaa pelaajaa tahtaamaan

    public GameObject cannon;
    public float x;
    public float y;
    public float z;

	void Start () {
        x = cannon.transform.position.x;
        y = cannon.transform.position.y;
        z = cannon.transform.position.z;

        transform.position = new Vector3(x, y, z);

        transform.eulerAngles = new Vector3(cannon.transform.eulerAngles.x, cannon.transform.eulerAngles.y, cannon.transform.eulerAngles.z);
    }

}
