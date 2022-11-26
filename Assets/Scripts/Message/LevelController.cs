using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    MessageControl messageControl;
    bool getKey = false;
    public GameObject desactivatePlayer;
    public GameObject desactivateCanvas;
    
    private void Update()
    {
        //Menu();
    }
    void Start(){
        //buttonAction = GetComponent<ButtonAction>();
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
            getKey = true;
        }
        if (other.gameObject.name.Equals ("ObjetivoEncontrarEstacionPolicia"))
        {
            Destroy(other.gameObject);
            messageControl.findPoliceStation();
            getKey = true;
        }
        //-----------MENSAJES DE DIALOGOS-----------
        if (other.gameObject.name.Equals ("DialogoEsperanza"))
        {
            Destroy(other.gameObject);
            messageControl.mensajeEsperanza();
            getKey = true;
        }
        if (other.gameObject.name.Equals ("DialogoSusto"))
        {
            Destroy(other.gameObject);
            messageControl.mensajeSusto();
            getKey = true;
        }
        if (other.gameObject.name.Equals ("DialogoMunicion"))
        {
            Destroy(other.gameObject);
            messageControl.mensajeMunicion();
            getKey = true;
        }
        if (other.gameObject.name.Equals ("DialogoCiudad"))
        {
            Destroy(other.gameObject);
            messageControl.mensajeCiudad();
            getKey = true;
        }
        if (other.gameObject.name.Equals ("DialogoHermana"))
        {
            Destroy(other.gameObject);
            messageControl.mensajeEncontrarHermana();
            getKey = true;
        }
        //-----------MENSAJES DE GUIA-----------
        if (other.gameObject.name.Equals ("Precaucion"))
        {
            Destroy(other.gameObject);
            messageControl.precaucionZombies();
            getKey = true;
        }
        if (other.gameObject.name.Equals ("GameOver"))
        {
            Destroy(other.gameObject);
            messageControl.gameOverMessage();
            getKey = true;
        }
    }/*
    void OnTriggerEnter(Collider other)
    {
        
    }*/

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

    public IEnumerator TimeToFreeze()
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.GetComponent<playerMov>().enabled = false;
        desactivatePlayer.SetActive(false);
        desactivateCanvas.SetActive(false);
    }
    
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
