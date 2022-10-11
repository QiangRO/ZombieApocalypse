using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public float rango = 5f;
    float danio;
    [SerializeField]
    Camera PlayerCam;

    [SerializeField]
    GameObject impactoBala;

    [SerializeField]
    ParticleSystem disparoFlash;

    [SerializeField]
    GameObject[] armas;

    bool activaArma1 = false, activaArma2 = false, activaArma3 = false, activaArma4 = false, activaDisparo = false;

    int arma1 = 30, arma2 = 50, arma3 = 35, arma4 = 15;

    void Update()
    {
        if (activaDisparo)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (activaArma1)
                {
                    arma1--;
                    Disparando();
                }
                if (activaArma2)
                {
                    arma2--;
                    Disparando();
                }

                if (activaArma3)
                {
                    arma3--;
                    Disparando();
                }

                if (activaArma4)
                {
                    arma4--;
                    Disparando();
                }

            }
        }

        if(arma1 == 0)
        {
            activaArma1 = false;
        }
        if(arma2 == 0)
        {
            activaArma2 = false;
        }
        if (arma3 == 0)
        {
            activaArma3 = false;
        }
        if (arma4 == 0)
        {
            activaArma4 = false;
        }


        if (Input.GetKeyDown("1"))
        {
            armas[0].SetActive(true);
            armas[1].SetActive(false);
            armas[2].SetActive(false);
            armas[3].SetActive(false);
            activaDisparo = true;
            activaArma1 = true;
            activaArma2 = false;
            activaArma3 = false;
            activaArma4 = false;
            danio = 5f;
        }

        if (Input.GetKeyDown("2"))
        {
            armas[0].SetActive(false);
            armas[1].SetActive(true);
            armas[2].SetActive(false);
            armas[3].SetActive(false);
            activaDisparo = true;
            activaArma1 = false;
            activaArma2 = true;
            activaArma3 = false;
            activaArma4 = false;
            danio = 10f;
        }

        if (Input.GetKeyDown("3"))
        {
            armas[0].SetActive(false);
            armas[1].SetActive(false);
            armas[2].SetActive(true);
            armas[3].SetActive(false);
            activaDisparo = true;
            activaArma1 = false;
            activaArma2 = false;
            activaArma3 = true;
            activaArma4 = false;
            danio = 15f;
        }

        if (Input.GetKeyDown("4"))
        {
            armas[0].SetActive(false);
            armas[1].SetActive(false);
            armas[2].SetActive(false);
            armas[3].SetActive(true);
            activaDisparo = true;
            activaArma1 = false;
            activaArma2 = false;
            activaArma3 = false;
            activaArma4 = true;
            danio = 40f;
        }

    }

    void Disparando()
    {
            disparoFlash.Play();
            RaycastHit hit;

            if (Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.forward, out hit, rango))
            {
                Debug.Log(hit.transform.name);
                Enemigo objeto = hit.transform.GetComponent<Enemigo>();
                Puerta puerta = hit.transform.GetComponent<Puerta>();

                if (objeto != null)
                {
                    objeto.accionDanio(danio);
                }
                if(puerta != null)
                {
                    puerta.AbrirPuerta();
                }
                GameObject impacto = Instantiate(impactoBala, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impacto, 0.2f);
            }
    }
}
