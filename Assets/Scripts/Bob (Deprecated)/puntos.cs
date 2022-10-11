using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class puntos : MonoBehaviour
{
    // Start is called before the first frame update
    public int punto;
    
    void Start()
    {
        ActualizarPuntos();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(punto);
    }

    void ActualizarPuntos()
    {
        TextReader leerArchivo;
        leerArchivo = new StreamReader("score.txt");
        punto = int.Parse(leerArchivo.ReadToEnd());
    }

    public void DarPuntos()
    {
        TextWriter archivo;
        archivo = new StreamWriter("score.txt");
        archivo.WriteLine(punto + 10);
        archivo.Close();
        ActualizarPuntos();
    }
}
