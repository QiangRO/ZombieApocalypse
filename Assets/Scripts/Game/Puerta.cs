using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void AbrirPuerta()
    {
        rb.MovePosition(new Vector3(transform.position.x, transform.position.y - 5, transform.position.z));
    }
}
