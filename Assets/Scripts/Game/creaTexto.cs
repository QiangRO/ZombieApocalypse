using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class creaTexto : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
       try
        {
            TextReader leerArchivo;
            leerArchivo = new StreamReader("score.txt");
            Debug.Log("Archivo leido.");
        }
        catch (Exception)
        {
            Debug.Log("CreandoArchivo.");
            TextWriter archivo;
            archivo = new StreamWriter("score.txt");
            
                archivo.WriteLine(0);
            
            archivo.Close();
            Debug.Log("El Archivo se a Creado");
        }

    }
}

