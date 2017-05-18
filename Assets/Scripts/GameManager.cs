using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* Script per gestire l'inizio dell'app/gioco
       @autor: E. Antolli
       @matricola: 131125
*/
public class GameManager : MonoBehaviour {
    
    public Canvas canvas;
    public AudioSource music;
    public GameObject groupCreditsUI;

    public void LoadScene(int scene)
    {
        //Si potrebbe fare direttamente, 
        //ma ho deciso che la classe GameManager doveva gestire l'evento, 
        //così abbiamo una gestione centralizzata (nello Start);
        StartCoroutine(LoadScene_(scene));
    }

    IEnumerator LoadScene_(int scene) {
        CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
            //Debug.Log("Diminuindo o alpha: " + canvasGroup.alpha);
        }
        //yield return new WaitForSeconds(1f);
        LoadScenes ls = canvas.GetComponent<LoadScenes>();
        ls.LoadScene(scene);
    }

    //Funzione che carica la schermata di crediti
    public void LoadHideCredits(bool state) {

        groupCreditsUI.SetActive(state);

    }

    //Funzione che chiude l'applicazione
    public void ExitApplication() {
        Application.Quit();
    }
}
