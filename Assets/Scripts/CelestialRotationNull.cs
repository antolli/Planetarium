using UnityEngine;
using System.Collections;

public class CelestialRotationNull: MonoBehaviour {

    Transform myTransform;
    void Start () {
        myTransform = GetComponent<Transform>();
    }
	
	
	void Update () {
        myTransform.rotation = Quaternion.identity;
        
    }
}