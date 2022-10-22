using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slownScale = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimeNormal()
    {
        Time.timeScale = 1f;
    }

    public void TimeSlown()
    {
        Time.timeScale = slownScale;
    }

    public void TimeStop()
    {
        Time.timeScale = 0f;
    }
}
