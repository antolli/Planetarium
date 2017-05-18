using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* Script per caricare il tutorial
       @autor: E. Antolli
       @matricola: 131125
    */
public class Tutorial : MonoBehaviour {
    
    public GameObject tutorialPart1;
    public GameObject tutorialPart2;
    public GameObject tutorial;
    public float tempo;
    float acc;
    int show;


    void Start()
    {
        acc = tempo;
        show = PlayerPrefs.GetInt("showTutorial");
        if (show == 0) {
            tutorialPart1.SetActive(true);
        }
    }

    void Update()
    {
        if(show == 0)
        {
            acc -= Time.deltaTime;
            if (acc <= 0)
            {
                if (tutorialPart1.activeSelf)
                {
                    tutorialPart1.SetActive(false);
                    tutorialPart2.SetActive(true);
                }
                else if (tutorialPart2.activeSelf)
                {
                    tutorialPart2.SetActive(false);
                    PlayerPrefs.SetInt("showTutorial", 1);
                }
                acc = tempo;
            }
            show = PlayerPrefs.GetInt("showTutorial");
        }
        
    }

    public void ShowSkipTutorial() {
        if (!tutorial.activeSelf)
        {
            tutorial.SetActive(true);
        }else
        {
            tutorial.SetActive(false);
        }
    }

    public void Skip(int part)
    {
        if (part == 1)
        {
            if (tutorialPart1.activeSelf) { 
                tutorialPart1.SetActive(false);
                tutorialPart2.SetActive(true);
            }
        }
        else if(part == 2)
        {
            if (tutorialPart2.activeSelf) { 
                tutorialPart2.SetActive(false);
                PlayerPrefs.SetInt("showTutorial", 1);
                show = PlayerPrefs.GetInt("showTutorial");
            }
        }
    }

}
