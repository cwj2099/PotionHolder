using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* *********************************************************************
 * DEVELOPER: Noah Young
 * DATE: 10/24/2022
 * LAST UPDATED: 10/27/2022
 * PURPOSE: Player tower class; should rotate and fire bullets. 
 * *********************************************************************
 */

public class TowerPlayer : Tower
{

    [Space(10)]
    [Header("Rotation")]
    private float _angleSpeed;        //  Speed at which the tower will rotate in relation to the mouse. 
    private float _angle;             //  The current angle of the tower. 

    private void Update()
    {
        //  Rotation. 
        RotateTower();

        //  Check for mouse input for firing.
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    //  ***TO-DO*** Fix this rotation code. 
    private void RotateTower()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var lookPos = mousePos - new Vector3(transform.position.x, 0, transform.position.z);

        Debug.Log(mousePos);

        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _angleSpeed);
    }
}
