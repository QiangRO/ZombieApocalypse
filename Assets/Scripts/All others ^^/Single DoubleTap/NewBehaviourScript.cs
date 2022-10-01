using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int tapTimes;
    public float resetTimer;
    public bool IsHoldingKey;

    IEnumerator resetTapTimes()
    {
        yield return new WaitForSeconds(resetTimer);
        tapTimes = 0;
    }

    private void FixedUpdate()
    {
        //SingleTap
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            tapTimes++;
            Debug.Log("Tap");
            StartCoroutine("resetTapTimes");
        }

        //Double Tap
        if (tapTimes >= 2)
        {
            tapTimes = 0;
            Debug.Log("DoubleTap");
        }

        //Holding Key
        if (Input.GetKey(KeyCode.Mouse0))
        {
            IsHoldingKey = true;
        }
        else
            IsHoldingKey = false;

        if (IsHoldingKey)
        {
            Debug.Log("IsHoldingDown");
        }
    }
}
