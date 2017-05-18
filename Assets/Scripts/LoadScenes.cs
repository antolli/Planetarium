using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/* Script per carricare le nuove scene
       @autor: E. Antolli
       @matricola: 131125
    */
public class LoadScenes : MonoBehaviour {
    
    public void LoadScene(int scene) {
        //Application.LoadLevel();
        if (scene == 1)
        {
            if (!PlayerPrefs.HasKey("showTutorial"))
            {
                PlayerPrefs.SetInt("showTutorial", 0);
            }

        }
        SceneManager.LoadScene(scene);
    }
}