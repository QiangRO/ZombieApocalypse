using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    MessageControl messageControl;
    TriggerControll triggerControll;

    public GameObject desactivatePlayer;
    public GameObject desactivateCanvas;
    
    private void Update()
    {
    }
    void Start()
    {
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        
    }*/
    void OnTriggerEnter(Collider other)
    {
        //-----------MENSAJES DE OBJETIVOS-----------
        if (other.gameObject.name.Equals ("ObjetivoEncontrarEscuela"))
        {
            Destroy(other.gameObject);
            messageControl.findSchool();

        }
        if (other.gameObject.name.Equals ("ObjetivoEncontrarEstacionPolicia"))
        {
            Destroy(other.gameObject);
            messageControl.findPoliceStation();

        }
        //-----------MENSAJES DE DIALOGOS PLAYER-----------
        if (other.gameObject.name.Equals ("DialogoEsperanza"))
        {
            Destroy(other.gameObject);
            messageControl.mensajeEsperanza();

        }
        if (other.gameObject.name.Equals ("DialogoSusto"))
        {
            Destroy(other.gameObject);
            messageControl.mensajeSusto();

        }
        if (other.gameObject.name.Equals ("DialogoMunicion"))
        {
            Destroy(other.gameObject);
            messageControl.mensajeMunicion();

        }
        if (other.gameObject.name.Equals ("DialogoCiudad"))
        {
            Destroy(other.gameObject);
            messageControl.mensajeCiudad();

        }
        if (other.gameObject.name.Equals ("EncontrarHermana"))
        {
            Destroy(other.gameObject);
            messageControl.mensajeEncontrarHermana();
        }
        if (other.gameObject.name.Equals ("Reflexion"))
        {
            Destroy(other.gameObject);
            messageControl.mensajereflexion();
        }
        //-----------MENSAJES DE DIALOGOS GIRL-----------
        if (other.gameObject.name.Equals ("hermanaMiedo"))
        {
            Destroy(other.gameObject);
            messageControl.girlMiedo();
        }
        if (other.gameObject.name.Equals ("hermanaNoCorras"))
        {
            Destroy(other.gameObject);
            messageControl.girlNoCorras();
        }
        //-----------MENSAJES DE GUIA-----------
        if (other.gameObject.name.Equals ("Precaucion"))
        {
            Destroy(other.gameObject);
            messageControl.precaucionZombies();
        }
        if (other.gameObject.name.Equals ("Municion"))
        {
            Destroy(other.gameObject);
            messageControl.precaucionMunicion();
        }
        if (other.gameObject.name.Equals ("Horda"))
        {
            Destroy(other.gameObject);
            messageControl.precaucionHorda();
        }
        if (other.gameObject.name.Equals ("Espera"))
        {
            Destroy(other.gameObject);
            messageControl.precaucionEsperaGirl();
        }
        if (other.gameObject.name.Equals ("GameOver"))
        {
            Destroy(other.gameObject);
            messageControl.gameOverMessage();

        }
    }
    /*
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Equals ("DoorArea"))
        {
            if (getKey)
            {
                StartCoroutine("TimeToFreeze");
                messageControl.onDoorWithKey();
                //buttonAction.Reintentar();
                Reintentar();
            }
            else
            {
                messageControl.onDoorWithoutKey();
            }
        }
    }*/
    /*
    public void gameOver()
    {
        gameObject.GetComponent<FPController>().enabled = false;
        StartCoroutine("TimeToFreeze");
        messageControl.gameOverMessage();
    }*/

    // public IEnumerator TimeToFreeze()
    // {
    //     yield return new WaitForSeconds(0.7f);
    //     gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    //     gameObject.GetComponent<playerMov>().enabled = false;
    //     desactivatePlayer.SetActive(false);
    //     desactivateCanvas.SetActive(false);
    // }
    
    /*
    public void Reintentar()
    {
        SceneManager.LoadScene("Retry");
    }
    public void Menu()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }*/
}
