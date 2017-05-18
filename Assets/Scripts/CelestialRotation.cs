using UnityEngine;
using System.Collections;

public class CelestialRotation : MonoBehaviour {

    Transform myTransform;
    public float speed;
    public float tilt;
   // GameManager manager;


    void Start () {
        myTransform = GetComponent<Transform>();
        myTransform.rotation = Quaternion.Euler(0f, 0f, tilt);
        //manager = GameObject.Find("Game manager").GetComponent<GameManager>();
    }
	
	void Update () {
        myTransform.Rotate(Vector3.up * speed * Time.deltaTime);
        //myTransform.RotateAround(Vector3.zero, Vector3.up, 0.1f * Time.deltaTime);
    }
}