using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageControl : MonoBehaviour
{
    //Tiempo que se muestra el mensaje
    [SerializeField]
    int timeToMessage;

    //Textos
    [SerializeField]
    Text guiaText;

    [SerializeField]
    Text objetivoText;

    [SerializeField]
    Text dialogoBoyText;

    [SerializeField]
    Text dialogoGirlText;

    //Backgrounds
    [SerializeField]
    GameObject panelGuia;

    [SerializeField]
    GameObject panelObjetivo;

    [SerializeField]
    GameObject panelDialogo;

    void Start()
    {
        StartCoroutine(TimeToMessageGuia());
        StartCoroutine(TimeToMessageObjetivo());
        StartCoroutine(TimeToMessageDialogo());
    }

    //****************************************************************************************************
    //Mensajes guias
    public void gameOverMessage()
    {
        guiaText.text = "GameOver";
        panelGuia.SetActive(true);
        StartCoroutine(TimeToMessageGuia());
    }

    public void precaucionZombies()
    {
        guiaText.text = "Los zombies te pueden matar";
        panelGuia.SetActive(true);
        StartCoroutine(TimeToMessageGuia());
    }

    public void missionComplete()
    {
        guiaText.text = "Objetivo completado!!";
        panelGuia.SetActive(true);
        StartCoroutine(TimeToMessageGuia());
    }

    //****************************************************************************************************
    //Mensajes objetivos
    public void findFireDepartament()
    {
        objetivoText.text = "Objetivo: busca a la niña en la estacion de bomberos";
        //panelObjetivo.SetActive(true);
        StartCoroutine(TimeToMessageObjetivo());
    }

    public void findPoliceStation()
    {
        objetivoText.text = "Objetivo: Busca a la niña en la estacion de policia";
        //panelObjetivo.SetActive(true);
        StartCoroutine(TimeToMessageObjetivo());
    }

    public void findSchool()
    {
        objetivoText.text = "Objetivo: Busca a la niña en la escuela";
        //panelObjetivo.SetActive(true);
        StartCoroutine(TimeToMessageObjetivo());
    }

    public void findRescueZone()
    {
        objetivoText.text = "Objetivo: Escapa de la ciudad";
        //panelObjetivo.SetActive(true);
        StartCoroutine(TimeToMessageObjetivo());
    }

    //****************************************************************************************************
    //Mensaje dialogos
    //-------PLAYER-------
    public void mensajeEsperanza()
    {
        dialogoBoyText.text = "Espero poder encontrar la escuela antes que me devoren los zombies";
        panelDialogo.SetActive(true);
        StartCoroutine(TimeToMessageDialogo());
    }

    public void mensajeSusto()
    {
        dialogoBoyText.text = "Maldita sea estos zombies son demasiado rapido";
        panelDialogo.SetActive(true);
        StartCoroutine(TimeToMessageDialogo());
    }

    public void mensajeMunicion()
    {
        dialogoBoyText.text = "Espero que encuentre munición para mi arma";
        panelDialogo.SetActive(true);
        StartCoroutine(TimeToMessageDialogo());
    }

    public void mensajeCiudad()
    {
        dialogoBoyText.text = "Esta ciudad es demasiado grande";
        panelDialogo.SetActive(true);
        StartCoroutine(TimeToMessageDialogo());
    }

    public void mensajeEncontrarHermana()
    {
        dialogoBoyText.text = "Tengo que encontrar a mi hermana, lo mas antes posible";
        panelDialogo.SetActive(true);
        StartCoroutine(TimeToMessageDialogo());
    }

    //-------GIRL-------
    public void girlMiedo()
    {
        dialogoGirlText.text = "Ten cuidado papa!!";
        panelDialogo.SetActive(true);
        StartCoroutine(TimeToMessageDialogo());
    }

    public void girlNoCorras()
    {
        dialogoGirlText.text = "No corras tan rapido papa!!";
        panelDialogo.SetActive(true);
        StartCoroutine(TimeToMessageDialogo());
    }

    public void girlEscucha()
    {
        dialogoGirlText.text = "Creo haber oido un helicoptero!!";
        panelDialogo.SetActive(true);
        StartCoroutine(TimeToMessageDialogo());
    }

    //****************************************************************************************************
    //TIEMPO PARA QUE DESAPAREZCA EL MENSAJE
    IEnumerator TimeToMessageGuia()
    {
        yield return new WaitForSeconds(timeToMessage);
        panelGuia.SetActive(false);
    }

    IEnumerator TimeToMessageObjetivo()
    {
        yield return new WaitForSeconds(timeToMessage);
        //panelObjetivo.SetActive(false);
    }

    IEnumerator TimeToMessageDialogo()
    {
        yield return new WaitForSeconds(timeToMessage);
        panelDialogo.SetActive(false);
    }
}
