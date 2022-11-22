using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObjects : MonoBehaviour
{
    int rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,rotSpeed,0));
    }
}
