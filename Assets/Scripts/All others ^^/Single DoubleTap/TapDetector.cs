using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TapDetector : MonoBehaviour
{
    public int tapTimes;
    public float resetTimer;
    public bool IsHoldingDown;

    public TextMeshPro Text;
    private bool SingleTap;
    private bool DoubleTap;

    IEnumerator ResetTapTimes()
    {
        yield return new WaitForSeconds(resetTimer);
        tapTimes = 0;
        SingleTap = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine("ResetTapTimes");
            tapTimes++;
            //SingleTap
            SingleTap = true;
            Text.SetText("SingleTap");
        }

        if (tapTimes >= 2)
        {
            tapTimes = 0;
            SingleTap = false;
            //DoubleTap
            DoubleTap = true;
            Text.SetText("DoubleTap");
        }

        if (Input.GetKey(KeyCode.Mouse0) && !SingleTap && !DoubleTap)
        {
            IsHoldingDown = true;
            //HoldDown
            Text.SetText("IsHoldingDown");
        }
        else
            IsHoldingDown = false;

        if (SingleTap && !DoubleTap)
            Text.SetText("SingleTap");

        if (DoubleTap)
            Text.SetText("DoubleTap");
    }
}
