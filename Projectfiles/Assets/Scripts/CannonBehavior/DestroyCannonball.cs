using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCannonball : MonoBehaviour {

    public float objectY;
    public float deadZone;
    public GameObject target;

    void Start()
    {
        deadZone = -5f;
    }

    void Update () {

        objectY = target.transform.position.y;

        // Tuhotaan kanuunankuula jos mennään alemmaksi kun deadzone
        if (objectY < deadZone)
        {
            Destroy(gameObject);
        }
	}
    // Tuhotaan objekti jos osutaan viholliseen
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Hitbox")
        {
            print("Tuhoa kuula." + this);
            Destroy(gameObject);
        }
    }

}
