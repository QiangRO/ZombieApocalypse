using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distancia : MonoBehaviour
{
    public Transform jugador;
    float dis;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector3.Distance(transform.position, jugador.position);
    }
}
