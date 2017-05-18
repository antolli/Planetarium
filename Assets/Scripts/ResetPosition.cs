using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/* Script che usa la stessa idea dello script "PlanetsView", però per confrontare se sono uscito
 * dal sistema solare, ovvero se sono perso 
       @autor: E. Antolli
       @matricola: 131125
    */
public class ResetPosition : MonoBehaviour {
    public float distance;
    public GameObject reference;
    public GameObject notice;
    Transform transformReference;
    Button btnReset;

    void Start() {
        transformReference = reference.GetComponent<Transform>();
        btnReset = GameObject.Find("BtnReset").GetComponent<Button>();
    }
	void Update () {
        /* Calcolo semplice per confrontare distanze
        * [P1(x,y,z) - P2(x,y,z)]^2 < radius^2
        * radius = distance
        * == anziche < vuol dire che è "on the circle"
        */
        Vector3 diff = transform.position - transformReference.position;
        float sqrLen = diff.sqrMagnitude;
        if (sqrLen > distance * distance)
        {
            if (!btnReset.interactable)
            {
                btnReset.interactable = true;
                if (!notice.activeSelf)
                    notice.SetActive(true);

            }
            //print("The other transform is very far from me!");

        }
        else {
            if (btnReset.interactable)
            {
                btnReset.interactable = false;
                if (notice.activeSelf)
                    notice.SetActive(false);
            }
        }
    }

    public void ResetMyPosition() {
        Rigidbody shuttle = reference.GetComponent<Rigidbody>();
        Quaternion rt = Quaternion.Euler(new Vector3(0, 90, 0));
        shuttle.MovePosition(new Vector3(760,0,0));
        shuttle.MoveRotation(rt);
    }

}
