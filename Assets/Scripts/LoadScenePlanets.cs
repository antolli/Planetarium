using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml;
using System.IO;
/* Script per carricare la scena "quiz" del pianeta salvato
       @autor: E. Antolli
       @matricola: 131125
    */
public class LoadScenePlanets : MonoBehaviour {

    public GameObject questions;
    public GameObject result;
    public GameObject score;
    public GameObject[] planets;
    public Text title;
    public int qtyQuestions;
 

    string whoIam;
    string path = Directory.GetCurrentDirectory();
    string correctAnswer;
    XmlDocument xmlDoc;
    int id = 0;
    int correct;
    int pts = 0;

	// Use this for initialization
	void Start () {
        whoIam = PlayerPrefs.GetString("planet");
        xmlDoc = new XmlDocument();
        xmlDoc.Load(path + "\\Xmls\\" + whoIam + ".xml");
        SetAppearance();
    }
    

    void SetAppearance() {
        //Riempie il titolo 
        title.text = xmlDoc.DocumentElement.SelectSingleNode("/planet/title").InnerText;
        //Ricupera l'id del pianeta
        int i = int.Parse(xmlDoc.DocumentElement.SelectSingleNode("/planet/game_object").InnerText);
        //Attivo il mio pianeta
        planets[i].SetActive(true);
        //Carica la prima domanda
        SetQuestion();
    }
    // Riempie le domande e le opzioni di risposta
    public void SetQuestion()
    {
        //nasconde tutto
        DisableAll();
        //prende la lista di questioni che ho per il pianeta
        XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("/planet/questions/question");
        //prende l'oggetto TxtQuestion
        Text question = questions.GetComponentInChildren<Text>();

        //trova la domanda che deve essere mostrata
        foreach (XmlNode node in nodes)
        {
            int idQuestion = int.Parse(node.Attributes["id"].InnerText);
            //id è un accumulatore che serve per capire che domanda deve essere mostrata all'utente
            if (idQuestion == id) {
                //inserisce l'enunciato della questione
                question.text = node.SelectSingleNode("title").InnerText;
                XmlNodeList answers = node.SelectNodes("answers/option");
                //inserisce le opzioni
                SetOptions(answers);
                //esce del ciclo
                break;
            }
        }
        //incrementa l'accumulatore
        id++;
        //verifica se sono finite le domande
        if (id > qtyQuestions)
        {
            //mostra lo score
            ShowScore();
        }
        else {
            //mostra la questione
            questions.SetActive(true);
        }
        
    }
    //Riempie le opzioni della domanda
     void SetOptions(XmlNodeList answers)
    {
        GameObject radios = questions.GetComponent<Transform>().FindChild("RadioButtons").gameObject;
        
        Toggle[] options = radios.GetComponentsInChildren<Toggle>();
        
        for (int i = 0; i < answers.Count; i++)
        {
            //Salva l'opzione giusta
            if (bool.Parse(answers[i].Attributes["correct"].InnerText)) {
                correct = int.Parse(answers[i].Attributes["idOption"].InnerText);
                correctAnswer = answers[i].SelectSingleNode("text").InnerText;
            }
            /* ho provato usare SetAllTogglesOff() del ToggleGroup, ma non andava (?)
             * allora ho dovuto usare isOn = false per poter disattivare tutte le opzioni che verrano mostrate
             * nella prossima domanda
             */
            options[i].isOn = false;

            // set option's text
            options[i].GetComponentInChildren<Text>().text = answers[i].SelectSingleNode("text").InnerText;
        }

    }

    /* Verifica se la risposta scelta è quella giusta.
     * Anche se è un obj toggle, uso una funzione che riceve un int come parametro perchè ho bisogno.
     * Allora ho creato una trigger, e non ho usato onValueChanged (default)
    */
    public void Verify(int resp)
    {
        questions.SetActive(false);

        Text txtResult = result.GetComponent<Transform>().FindChild("TxtResult").GetComponent<Text>();
        Text txtAnswer = result.GetComponent<Transform>().FindChild("TxtAnswer").GetComponent<Text>();
        
        if (resp == correct){
            txtResult.text = "Correct";
            txtResult.color = Color.green;
            txtAnswer.text = correctAnswer;
            pts++;
        }
        else{
            txtResult.text = "Wrong";
            txtResult.color = Color.red;
            txtAnswer.text = correctAnswer;
        }

        result.SetActive(true);
    }
    
    void DisableAll() {
        questions.SetActive(false);
        result.SetActive(false);
        score.SetActive(false);
        
    }
    //mostra lo score
    void ShowScore() {
        Text txtPts = score.GetComponent<Transform>().FindChild("TxtPts").GetComponent<Text>();
        txtPts.text = pts + "/" + qtyQuestions;
        score.SetActive(true);
    }
    //ricomincia da capo
    public void Reload() {
        pts = 0;
        id = 0;
        SetQuestion();
    }
}
