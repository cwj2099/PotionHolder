using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwaner : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private void OnEnable()
    {
        if (target != null)
        {
            target.SetActive(true);
        }
    }
}
