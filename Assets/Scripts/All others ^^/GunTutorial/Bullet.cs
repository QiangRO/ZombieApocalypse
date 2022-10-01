using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform cam;
    public float speed;
    public float spread;
    

    private void Awake()
    {
        cam = GameObject.Find("fps cam").transform;

        //Random Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        float z = Random.Range(-spread, spread);
        transform.Rotate(new Vector3(x, y, z));
    }
    private void Update()
    {
        transform.Translate(cam.forward * speed * Time.deltaTime);
    }
}
