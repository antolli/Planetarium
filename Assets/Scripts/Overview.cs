using UnityEngine;
using System.Collections;
/* Script per gestire l'overview
      @autor: E. Antolli
      @matricola: 131125
   */
public class Overview : MonoBehaviour {

    public GameObject refMe;
    RectTransform  rectTransMe;
    float radiusOverview = 100f;
    float radiusSolarSystem = 15000f;
    float x, y;

    /* Relazione di equivalenza tra Vector3 GameObjects e Overview (--> = "sta per"):
     * 
     * Real  vs. Ref (Overview)
     *  x   -->  y
     *  z   -->  x
     *   
      */

    void Start () {
        rectTransMe =  refMe.GetComponent<RectTransform>();
    }
	

	void Update () {
        // Calcola la posizione dell'oggetto
        CalculatePositions(transform, rectTransMe);

    }
    public void CalculatePositions(Transform transformReal, RectTransform transformRef) {
        
        //Prende la posizione dell'oggetto "reale"  
        Vector3 realPosition = transformReal.position;
        //Vector2 refPosition = transformRef.anchoredPosition;
        //Debug.Log("Real Pos: " + realPosition + "| Ref Pos: " + refPosition);

        /* I calcoli si basano sui raggi delle rappresentazioni del sistema solare 
         * per poter fare la proporzione, dunque le ancore devono per forza stare 
         * insieme e posizionate al centro del circolo Overview;
         * In base a questo, anchorMin e anchorMax devono avere il valore: Vector2(0.0,0.0).
         */
         y = ((realPosition.x) * radiusOverview) / radiusSolarSystem;
         x = ((realPosition.z) * radiusOverview) / radiusSolarSystem;

        //Imposta un limite; 
        if (Limit(x, y))
        {
            //Attribuisco la posizione proporzionale all'oggetto di riferimento
            transformRef.anchoredPosition = new Vector2(-x, y);
        }

        

    }
    bool Limit(float x, float y)
    {
        float sqrX = Mathf.Pow((x - 0), 2);
        float sqrY = Mathf.Pow((y - 0), 2);
        float sum = sqrX + sqrY;
        float cnfr = radiusOverview * radiusOverview;
        if (sum > cnfr) {
            return false;
        }
        else {
            return true;
        }
    }

    

}
