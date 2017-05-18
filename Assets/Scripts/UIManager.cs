using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/* Script per gestire la UI
    @autor: E. Antolli
    @matricola: 131125
 */
public class UIManager : MonoBehaviour {

    public GameObject overview;
    public Button btnAudio;
    public Sprite spriteWithAudio, spriteWithoutAudio;
    
    Image srcImg;
    AudioSource music;

    void Start() {

        srcImg = btnAudio.GetComponent<Image>();
        music = GameObject.Find("Game manager").GetComponent<AudioSource>();
    }
    /* Metodo chiamato dal tasto Overview 
     * nasconde e mostra la finestra di overview 
     */
    public void ShowHideOverview()
    {
        bool act = overview.activeSelf;
        if (act)
        {
            overview.SetActive(false);
        }
        else {
            overview.SetActive(true);
        }
    }
    /* Metodo chiamato dal tasto Mute
     * imposta l'audio come 'mute' o no
     * cambia l'immagine del tasto
     */
    public void MuteAudio()
    {
        if (music.mute)
        {
            music.mute = false;
            srcImg.sprite = spriteWithAudio;
        }
        else
        {
            music.mute = true;
            srcImg.sprite = spriteWithoutAudio;
        }
    }
}