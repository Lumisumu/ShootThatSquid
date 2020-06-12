using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : MonoBehaviour {

    // Keskipiste
    public GameObject Target; // Mikä gameobjektia käytetään keskipisteena
    private Vector3 pivotPoint; // Tähän tallennetaan Targetin Vector3

    public float speed; // Kääntymis nopeus

    // Kanuunan kääntämisen säätö
    public float maxVerticalX;
    public float minVerticalX;
    public float maxHorizontalY;
    public float minHorizontalY;
    public float currentX; // Mikä on kanuunan tämän hetkinen rotaatio jotta voimme käyttää tätä max-min vertical Y vertailuun
    public float currentY;



	void Start () {
        speed = 30f;
    }
    
    void Update () {
        pivotPoint = Target.transform.position;
        currentX = transform.localEulerAngles.x;
        currentY = transform.localEulerAngles.y;

        /* VANHAT KONTROLLIT
            // Kanuunan horizontal liikkuminen
        if ( Input.GetKey(KeyCode.LeftArrow))
        {
            if (currentY >= maxHorizontalY)
            {
                transform.RotateAround(pivotPoint, Vector3.up, -speed * 2 * Time.deltaTime);
            }
        }

        if (  Input.GetKey(KeyCode.RightArrow))
        {
            if (currentY <= minHorizontalY)
            {
                transform.RotateAround(pivotPoint, Vector3.up, speed * 2 * Time.deltaTime);
            }
        }

        // Kanuunan vertical liikkuminen
        if ( Input.GetKey(KeyCode.UpArrow))
        {
            if (currentX < maxVerticalX)
            {
                transform.Rotate(Vector3.right * Time.deltaTime * speed * 2, Space.Self);
            }
        }

        if ( Input.GetKey(KeyCode.DownArrow))
        {
           if (currentX > minVerticalX)
            {
                transform.Rotate(Vector3.right * Time.deltaTime * -speed * 2, Space.Self);
            }
        }
        */


        // ***UUDET KONTROLLIT***
        // Kanuunan horizontal liikkuminen, XboxOne
        if (Input.GetAxis("VerticalTwo") == 1  || Input.GetKey(KeyCode.LeftArrow))
        {
            if (currentY >= maxHorizontalY)
            {
                transform.RotateAround(pivotPoint, Vector3.up, -speed * 2 * Time.deltaTime);
                Quaternion q = transform.rotation;
                q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
                transform.rotation = q;
            }
        }

        if (Input.GetAxis("VerticalTwo") == -1 || Input.GetKey(KeyCode.RightArrow))
        {
            if (currentY <= minHorizontalY)
            {
                transform.RotateAround(pivotPoint, Vector3.up, speed * 2 * Time.deltaTime);
                Quaternion q = transform.rotation;
                q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
                transform.rotation = q;
            }
        }

        // Kanuunan vertical liikkuminen, XboxOne
        if (Input.GetAxis("HorizontalTwo") == 1 || Input.GetKey(KeyCode.UpArrow))
        {
            if (currentX <= maxVerticalX)
            {
                transform.Rotate(Vector3.right * Time.deltaTime * speed * 2, Space.Self);
            }
        }

        if (Input.GetAxis("HorizontalTwo") == -1 || Input.GetKey(KeyCode.DownArrow))
        {
           if (currentX >= minVerticalX)
            {
                transform.Rotate(Vector3.right * Time.deltaTime * -speed * 2, Space.Self);
            }
        }
        // ***UUDET KONTROLLIT LOPPU***


        if (currentX > maxVerticalX)
        {
            currentX = maxVerticalX;
        }

        if (currentX < minVerticalX)
        {
            currentX = minVerticalX;
        }


        if(currentY > maxHorizontalY)
        {
            currentY = maxHorizontalY;
        }

        if(currentY < minHorizontalY)
        {
            currentY = minHorizontalY;
        }

        //transform.localRotation = Quaternion.Euler(currentX, currentY, 0);
    }
}
