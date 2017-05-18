using UnityEngine;
using System.Collections;
/* Script per fare test
       @autor: E. Antolli
       @matricola: 131125
    */
public class RestartPrefs_JustToTesting : MonoBehaviour {

    public void Destroy() {
        PlayerPrefs.DeleteAll();
    }
}
