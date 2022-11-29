using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TriggerControll : MonoBehaviour
{
    public GameObject desactivatePlayer;
    public GameObject desactivateCanvas;

    [SerializeField]
    MessageControl messageControl;

    [SerializeField]
    GameObject dialogosNiña;

    [SerializeField]
    GameObject dialogosGuia;

    [SerializeField]
    GameObject cajasMunicion;

    [SerializeField]
    GameObject policeArea;

    [SerializeField]
    GameObject fireArea;

    [SerializeField]
    GameObject girlArea;

    [SerializeField]
    GameObject rescueArea;
    bool objPolice = false;
    bool objFire = false;
    bool objGirl = false;
    bool objRescue = false;

    void Start() { }

    void Update() { }

    void OnTriggerEnter(Collider other)
    {
        //Objetivo ir a la estacion de policia
        if (other.gameObject.name.Equals("PoliceArea"))
        {
            objPolice = true;
            Destroy(other.gameObject);
            fireArea.SetActive(true);
            messageControl.findFireDepartament();
            messageControl.missionComplete();
        }
        //Objetivo ir a la estacion de bomberos
        if (other.gameObject.name.Equals("FireArea") && objPolice)
        {
            objFire = true;
            Destroy(other.gameObject);
            girlArea.SetActive(true);
            messageControl.findSchool();
            messageControl.missionComplete();
        }
        //Objetivo ir a la escuela
        if (other.gameObject.name.Equals("GirlArea") && objFire && objPolice)
        {
            objGirl = true;
            Destroy(other.gameObject);
            rescueArea.SetActive(true);
            dialogosNiña.SetActive(true);
            cajasMunicion.SetActive(true);
            dialogosGuia.SetActive(true);
            messageControl.findRescueZone();
            messageControl.missionComplete();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Equals("RescueArea") && objFire && objPolice && objGirl)
        {
            StartCoroutine("TimeToFreeze");
            StartCoroutine("ChangeScene");
            messageControl.winMessage();
        }
    }

    public IEnumerator TimeToFreeze()
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.GetComponent<playerMov>().enabled = false;
        desactivatePlayer.SetActive(false);
        desactivateCanvas.SetActive(false);
    }
    private IEnumerator ChangeScene(){
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Menu");
    }
}
