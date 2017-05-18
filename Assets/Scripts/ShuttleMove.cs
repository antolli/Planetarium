using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Script per gestire il movimento del shuttle
       @autor: E. Antolli
       @matricola: 131125
    */

public class ShuttleMove : MonoBehaviour {
    

    Rigidbody shuttle;

    float translationSpeed;

    bool pressed;

    public float rotationSpeed;
    public GameObject effectMove;
    /*
     Siccome è un progetto semplice questa parte è abbastanza statica:
     Deffinisco 3 tipi di velocità
         */
    public GameObject stopped, speedOne, speedTwo, speedThree;

    void Start () {
        //Inizializzo gli attr. privati
        shuttle = GetComponent<Rigidbody>();
        pressed = false;

        //Verifico se ho salvato la mia posizione X
        if (PlayerPrefs.HasKey("shuttlePositionX"))
        {
            shuttle.position = new Vector3(PlayerPrefs.GetFloat("shuttlePositionX"), 0, 0);
        }
    }
	
	void Update () {

        // Verifica se il tasto "ctrl" è premuto o se c'è il click del mouse
        //ovvero se l'utente sta 'accelerando' 
        if (Input.GetButtonDown("Fire1"))
        {
            pressed = true;
        }
        if (Input.GetButtonUp("Fire1")){

            pressed = false;
        }
        
        //Chiama il metodo che 'crea' l'accelerazione
        Acceleration(pressed);

        //Muov. asse Y (orizzontale) A/D  ←/→
        //Mouv. asse Y (orizzontale) MOUSE ←/→
        //float yaw = Input.GetAxis("Horizontal");
        float yaw = Input.GetAxis("Mouse X");
        // Muov. asse X (look up/down) W/S  ↑/↓
        //Mouv. asse X (look up/down) MOUSE ↑/↓
        //float pitch = Input.GetAxis("Vertical");
        float pitch = Input.GetAxis("Mouse Y");

        //Calcola come l'oggetto realizza il controllo visuale Pitch (come fare 'si' con la testa)
        //fixedDeltaTime perchè ci si tratta di un RigidBody
        Quaternion deltaPitch = Quaternion.Euler(Vector3.right * -pitch * rotationSpeed * Time.fixedDeltaTime);

        //Calcola come l'oggetto deve muoversi avanti
        //fixedDeltaTime perchè ci si tratta di un RigidBody
        Vector3 deltaFwd = transform.forward * translationSpeed * Time.fixedDeltaTime;

        //Calcola come l'oggetto deve muoversi ruotando in torno all'asse Y (come fare 'no' con la testa)
        //fixedDeltaTime perchè ci si tratta di un RigidBody
        Quaternion deltaYaw = Quaternion.Euler(Vector3.up * yaw * rotationSpeed * Time.fixedDeltaTime);

        //se l'utente 'accelera', il shuttle si muove 
        if (pressed)
        {
            effectMove.SetActive(true);
            shuttle.MovePosition(shuttle.position + deltaFwd);
            shuttle.MoveRotation(shuttle.rotation * deltaYaw);
            shuttle.MoveRotation(shuttle.rotation * deltaPitch);

        }
        else {
            effectMove.SetActive(false);
        }

    }

    void Acceleration(bool pressed)
    {
        if (pressed){
            if (translationSpeed <= 300)
            {
                translationSpeed += Time.deltaTime * 5;
                UpdatePanelAcceleration();
            }
        }else{
            translationSpeed = 0f;
            UpdatePanelAcceleration();
        }
    }

    //Aggiorna il panelo con le velocità
    void UpdatePanelAcceleration() {
        if (translationSpeed == 0f){
            speedOne.SetActive(false);
            speedTwo.SetActive(false);
            speedThree.SetActive(false);
            stopped.SetActive(true);
        }
        else if (translationSpeed > 0.0f && translationSpeed <= 50f) {
            stopped.SetActive(false);
            speedOne.SetActive(true);
        }else if (translationSpeed > 50.0f && translationSpeed <= 150f){
            speedTwo.SetActive(true);
        }else if (translationSpeed > 150.0f && translationSpeed <= 300f){
            speedThree.SetActive(true);
        }
    }
    
}