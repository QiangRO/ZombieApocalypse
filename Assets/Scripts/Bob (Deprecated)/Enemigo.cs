using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float salud = 40f;
    public puntos puntuacion;

    public void Start()
    {
        puntuacion = GameObject.FindGameObjectWithTag("Terreno").GetComponent<puntos>();
    }

    public void accionDanio(float danio)
    {
        salud -= danio;
        if (salud <= 0)
        {
            puntuacion.DarPuntos();
            Destroy(gameObject);
        }
    }
}
