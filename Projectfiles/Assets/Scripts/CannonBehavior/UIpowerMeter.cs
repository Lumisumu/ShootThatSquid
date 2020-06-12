using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIpowerMeter : MonoBehaviour {

    public GameObject target; // Käytetään kanuunan hakemiseen, jotta voimme tarkistella tässä koodissa launch power dataa

    // Mittarit graafinen
    public GameObject meter;
    public GameObject maxMeter;
    public GameObject lastPointer; // Legacy
    public GameObject lastMeter; // Näytetään pelaajan viime ammuksen voima
    public GameObject reloadtext;

    // Numerot
    public float muchPower;
    public float multiply; // Koska lopullisessa versiossa muutamme kuinka paljon kanuuna voi käyttää voimaa, niin on helpompi että tämä on säädettävissä
    public float endPower; // Maximi poweri, mutta en halunnu käyttää maxPower joka on sama variable name LaunchBehaviorissa, selkeyden vuoksi
    public float lastLaunch; // Aiempi laukaisu voimakkuus 
    public float reloadTime;


	void Update () {
        // Tämä tarkistetaan joka frame, koska power variable on muuttuva
        var getData = target.GetComponent<LaunchBehavior>(); // Tässä haemme kanuunan skriptin jota se käyttää
        muchPower = getData.power; // Otetaan power data muistiin.

        meter.transform.localScale = new Vector3(0.20f, (muchPower-100)*multiply, 0.1f);

        // Haetaan maximi poweri mittaria varten
        var getMaxPower = target.GetComponent<LaunchBehavior>();
        endPower = getMaxPower.maxPower;

        // Muutetaan takana olevan voima baarin koko
        maxMeter.transform.localScale = new Vector3(0.20f, (endPower-100) * multiply, 0.1f);

        // Last Pointer
        var getLastLaunch = target.GetComponent<LaunchBehavior>();
        lastLaunch = getLastLaunch.lastPower;

        //lastPointer.transform.localPosition = new Vector3(450f, (lastLaunch * multiply) - 255, 0); // LEGACY VERSIO
        lastMeter.transform.localScale = new Vector3(0.20f, (lastLaunch - 100) * multiply, 0.1f);

        // Reload
        var getReload = target.GetComponent<LaunchBehavior>(); // Piirretään reloading teksti tarvittaessa
        reloadTime = getReload.reload;

        if (reloadTime > 0)
        {
            reloadtext.SetActive(true);
        }
        else
        {
            reloadtext.SetActive(false);
        }
    }
}
