using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ObjectCurve : MonoBehaviour
{
    public string targetTag;
    public GameObject targetObject;
    Vector3 _iniPosition;
    public float travelTime=1;
    float t = 0;
    public bool disableOnReached;
    public bool faceCamera;
    // Start is called before the first frame update
    void Start()
    {
       targetObject = GameObject.FindGameObjectWithTag(targetTag);
        _iniPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject != null)
        {
            t += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(_iniPosition, targetObject.transform.position, t / travelTime);
        }

        if (t >= travelTime)
        {
            gameObject.SetActive(false);
        }

        if (faceCamera)
        {
            transform.LookAt(Camera.main.transform.position, Vector3.up);
        }
    }
}
