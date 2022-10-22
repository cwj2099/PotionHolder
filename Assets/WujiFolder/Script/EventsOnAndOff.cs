using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsOnAndOff : MonoBehaviour
{
    public UnityEvent EnableEvents;
    public UnityEvent DisableEvents;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        EnableEvents.Invoke();
    }

    private void OnDisable()
    {
        DisableEvents.Invoke();
    }
}
