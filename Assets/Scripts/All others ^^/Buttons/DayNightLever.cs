using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightLever : MonoBehaviour
{
    public DayNight_Manager manager;
    public bool isDay;
    private void OnMouseDown()
    {
        if (!isDay)
        manager.SwitchToDay();
        else 
        manager.SwitchToNight();

        isDay = !isDay;
    }
}
