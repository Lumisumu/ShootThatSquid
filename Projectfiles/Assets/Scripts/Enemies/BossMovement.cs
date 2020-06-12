using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {

    // Perusvihollisen liikkuminen, liikkuu valittuun targettiin päin (pelaaja)
    public GameObject whoTarget;
    public float movementSpeed;
    public float maxDistance; // Kuinka lähelle pelaajaa monsteri voi liikkua

    // Variaablit
    public Transform target;
    public float rotSpeed = 60f;
    public float lockAngle = 0f;

    public int knockbackTimer;

    //Saa roundin numeron ja laittaa sen nopeuteen
    public GameObject roundSystem;
	public RoundManager roundManager;

    void Start () {
        knockbackTimer = 0;

        //Etsii pelaajan
        whoTarget = GameObject.FindGameObjectWithTag("Player");
        target = whoTarget.transform;

        roundSystem = GameObject.FindGameObjectWithTag("RoundSystem");
		roundManager = roundSystem.GetComponent<RoundManager>();

        if(movementSpeed > 9){
            movementSpeed = 9;
        }
	}
	
	void Update () {

        // Rajoitetaan liikkuminen vain haluttuihin akseleihin
        var targetPosition = whoTarget.transform.position;

        // Objekti voi liikkua vain x ja z akselissa
        var limitedPosition = new Vector3(targetPosition.x, 0, targetPosition.z);
        float currentDistance = Vector3.Distance(transform.position, targetPosition);

        // Liikutaan whoTargettiin päin
        float step = movementSpeed * Time.deltaTime; // Time.deltaTime Use this function to make your game frame rate independent.

        //Knockback check
            if(knockbackTimer <= 0){

                // Monsteri liikkuu vain tietylle pituudelle
                if (currentDistance > maxDistance){
                // print("Distance: " + currentDistance + " MaxDistance: " + maxDistance);
                   transform.position = Vector3.MoveTowards(transform.position, limitedPosition, step);
                }
            }
            else{
                knockbackTimer--;
            }

        // Rotatetaan modellia targettiin päin
        var q = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(lockAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    // Debug
    void OnDrawGizmos()
    {
        Color color;
        color = Color.green;
        // local up
        DrawHelperAtCenter(this.transform.up, color, 2f);

        color.g -= 0.5f;
        // global up
        DrawHelperAtCenter(Vector3.up, color, 1f);

        color = Color.blue;
        // local forward
        DrawHelperAtCenter(this.transform.forward, color, 2f);

        color.b -= 0.5f;
        // global forward
        DrawHelperAtCenter(Vector3.forward, color, 1f);

        color = Color.red;
        // local right
        DrawHelperAtCenter(this.transform.right, color, 2f);

        color.r -= 0.5f;
        // global right
        DrawHelperAtCenter(Vector3.right, color, 1f);
    }

    private void DrawHelperAtCenter(
                       Vector3 direction, Color color, float scale)
    {
        Gizmos.color = color;
        Vector3 destination = transform.position + direction * scale;
        Gizmos.DrawLine(transform.position, destination);
    }

    public void AddKnockback () {
        knockbackTimer = 30;
    }
}
