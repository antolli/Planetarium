using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/* Script per verificare la distanza shuttle-pianeta; salva l'info che serve alla view del pianeta 
       @autor: E. Antolli
       @matricola: 131125
    */
public class PlanetsView : MonoBehaviour {

    public float closeDistance;
    public string myNameIs;
    AudioSource musicDefault;
    Transform shuttle;
    Button btnInfo;
    bool imClose = false;

    //attributo utile per capire se era già impostato come mute
    bool iWasMute;

    void Start () {
        shuttle = GameObject.FindGameObjectWithTag("Shuttle").GetComponent<Transform>();
        btnInfo = GameObject.Find("BtnInfo").GetComponent<Button>();
        musicDefault = GameObject.Find("Game manager").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        CalculateDistance();
        
    }
    void CalculateDistance() {
        /* Calcolo semplice per confrontare distanze
         * [P1(x,y,z) - P2(x,y,z)]^2 < radius^2
         * radius = closeDistance
         * == anziche < vuol dire che è "on the circle"
         */
        Vector3 diff = shuttle.position - transform.position;
        float sqrLen = diff.sqrMagnitude;
        if (sqrLen < closeDistance * closeDistance)
        {
            if (imClose == false) {
                //print("The other transform is close to me!");
                btnInfo.interactable = true;

                if (musicDefault.mute){
                    iWasMute = true;
                }
                else{
                    iWasMute = false;
                    musicDefault.mute = true;
                }

                btnInfo.GetComponent<AudioSource>().Play();
                PlayerPrefs.SetString("planet", myNameIs);
                imClose = true;
            }       
        }
        else {
            if (imClose)
            {
                btnInfo.interactable = false;
                btnInfo.GetComponent<AudioSource>().Stop();
                if (iWasMute == false)
                    musicDefault.mute = false;

                imClose = false;
            }
            
        }
    }
}
