using UnityEngine;
using System.Collections;
/* Script per salvare lo stato di "explore"
       @autor: E. Antolli
       @matricola: 131125
    */
public class SaveState : MonoBehaviour {

    public GameObject shuttle;


	public void Save(int scene)
    {
        LoadScenes ls = transform.GetComponent<LoadScenes>();
        Transform transfShuttle = shuttle.GetComponent<Transform>();
        /* Salvo soltanto la posizione X dello shuttle, in modo da tornare alla scena "Explore" 
         * più o meno vicino al pianeta che ho "cliccato". 
         * (E' un modo da aggirare la situazione senza complicare lo script)
         */
        PlayerPrefs.SetFloat("shuttlePositionX", transfShuttle.position.x);
        ls.LoadScene(scene);

    }
}
