using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControll : MonoBehaviour
{
    [SerializeField]
    MessageControl messageControl;

    [SerializeField]
    GameObject dialogosNiña;

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
            messageControl.findRescueZone();
            messageControl.missionComplete();
        }
        //Objetivo rescatar a la niña
        if (other.gameObject.name.Equals("RescueArea"))
        {
            Destroy(other.gameObject);
            objRescue = true;
        }
        /*void OnTriggerStay(Collider other)
        {
            if (other.gameObject.name.Equals("RescueArea"))
            {
                if (objFire && objPolice && objGirl)
                {
                    StartCoroutine(4f);
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
    }
}
