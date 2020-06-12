using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotPlacement : MonoBehaviour {
    //Tykin Pivot

    public Transform pivotTarget;
    public float offset;
    private float pivotX;
    private float pivotY;
    private float pivotZ;
 

    void Start () {
        offset = -0.48f;
	}
	
	void Update () {
        pivotX = pivotTarget.transform.position.x;
        pivotY = pivotTarget.transform.position.y;
        pivotZ = pivotTarget.transform.position.z;
        transform.position = new Vector3(pivotX, pivotY-offset, pivotZ);
    }
}
