using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBehavior : MonoBehaviour {

    //Viitatut objektit
    public GameObject target; //Mistä kuula ilmantuu
    public GameObject Bullet;

    public bool ladattu = false;

    // Kuulan variablet
    public float power;
    public float maxPower;
    public float minPower;
    public float lastPower; // Tallennetaan edellisen laukaisun voimakkuus jotta pelaaja voi helpommin verrata sitä pelissä
    public bool powerUp; // Kasvaako vai laskeeko voima
    public float reload;

    public float x;
    public float y;
    public float z;
    public Vector3 launchAngle;

    //Ääniefekti ampuessa
    AudioSource audio;
    public AudioClip shoot;

    void Start () {

        // Power aloitus asetukset
        maxPower = 250f; // Kuulan voimakkuuden maksimi pontenttiaali
        minPower = 100f;   // (ÄLÄ aseta minuksen puolelle, tai kuula lentää taaksepäin)
        power = 100f;      // Nykyinen kuulan laukaisu voima
        powerUp = true;  // Lisätäänkö voimaa vai vähennetäänkö
        reload = 0;

        audio = GetComponent<AudioSource>();
    }
	
	void Update () { // Laukaisu

        // Saadaan launchpointin rotaatio
        x = target.transform.position.x;
        y = target.transform.position.y;
        z = target.transform.position.z;

        // Mikä angle
        launchAngle = new Vector3(x, y, z);

        // Säädettävä voima kun spacebar on alhaalla
        if (reload <= 0)
        {
            if (Input.GetKey(KeyCode.JoystickButton0) || Input.GetKey(KeyCode.Space))
            {
                ladattu = true;
                // Checkataan onko ghosteja olemassa ja tuhotaan ne
                if (GameObject.FindWithTag("Ghost"))
                {
                    Destroy(GameObject.FindWithTag("Ghost"));
                }
                // Tsekataan lisätäänkö vai vähennetäänkö voimaa
                if (powerUp == true)
                {
                    // Voimaa lisätään
                    power += 1;
                    if (power >= maxPower)
                    {
                        power = maxPower;
                        powerUp = false;
                    }
                }
                if (powerUp == false)
                {
                    // Voimaa vähennetään
                    //    power -= 1;
                    if (power <= minPower)
                    {
                        power = minPower;
                        powerUp = true;
                    }
                }

            }
        }

        // Luodaan kopio bullet gameobjektista kun Space painike nostetaan, muutetaan se aktiiviseksi ja lisätään siihen voimaa
        if (reload <= 0 && ladattu == true)
        {
            if (Input.GetKeyUp(KeyCode.JoystickButton0) || Input.GetKeyUp(KeyCode.Space))
            {
                // Ensimmäisenä otetaan talteen voimakkuus jotta pelaaja voi verrata sitä seuraavaan laukaisuun
                lastPower = power;
                // Kloonataan alkuperäinen objekti ja launchataan klooni
                GameObject clone = Instantiate(Bullet, launchAngle, Quaternion.identity);
                audio.PlayOneShot(shoot, 0.7F);
                clone.active = enabled;
                clone.GetComponent<Rigidbody>().AddForce(transform.up * power);

                power = minPower;
                powerUp = true;
                reload = 45;
                ladattu = false;
            }
        }
        else
        { reload--; }
    }

}
