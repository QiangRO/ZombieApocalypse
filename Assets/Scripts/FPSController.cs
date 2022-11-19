using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField]
    float walkSpeed = 3.0f;

    [SerializeField]
    float runSpeed = 6.0f;
    
    [SerializeField]
    Camera cam;

    [SerializeField]
    float mouseHorizontal = 2.0f;

    [SerializeField]
    float mouseVertical = 2.0f;

    [SerializeField]
    float minRotation = -65.0f;

    [SerializeField]
    float maxRotation = 60.0f;

    float h_mouse , v_mouse;

    Vector3 move = Vector3.zero;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        h_mouse = mouseHorizontal * Input.GetAxis("Mouse X");
        v_mouse += mouseVertical * Input.GetAxis("Mouse Y");

        v_mouse = Mathf.Clamp(v_mouse, minRotation, maxRotation);

        cam.transform.localEulerAngles = new Vector3(-v_mouse, 0, 0);

        transform.Rotate(0, h_mouse, 0);

        //////////////////////////////////////Movimiento////////////////////////////////

        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if(Input.GetKey(KeyCode.LeftShift)){
            transform.Translate(move * runSpeed * Time.deltaTime);
        } else {
            transform.Translate(move * walkSpeed * Time.deltaTime);
        }

    }
}
