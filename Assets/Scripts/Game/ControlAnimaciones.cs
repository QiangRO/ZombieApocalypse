using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ControlAnimaciones : MonoBehaviour
{
    public Animator anim;
    string animacionActual = null;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Jugador")

        {
            anim.SetBool("Ataca", true);
        }
        
    }


}
