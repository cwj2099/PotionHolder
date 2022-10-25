using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* *********************************************************************
 * DEVELOPER: Noah Young
 * DATE: 10/24/2022
 * PURPOSE: Player tower class; should rotate and fire bullets. 
 * *********************************************************************
 */

public class TowerPlayer : TowerBase
{
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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.y = 0;
        mousePos.y = 0;

        Vector2 lookDir = worldPos - transform.position;

        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, angle, 0);
    }
}
