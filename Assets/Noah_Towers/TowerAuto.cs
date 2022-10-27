using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* *********************************************************************
 * DEVELOPER: Noah Young
 * DATE: 10/27/2022
 * LAST UPDATED: 10/27/2022
 * PURPOSE: Tower class for all non-player towers. 
 * *********************************************************************
 */
public class TowerAuto : Tower
{

// ********************************************************************************************************************************

    /// <summary>
    /// AutoFire() will tell the Tower to fire at the assigned rate of fire.
    /// </summary>
    /// <param name="rate">The rate of fire.</param>
    protected IEnumerator AutoFire(float rate)
    {
        while (true)
        {
            yield return new WaitForSeconds(rate);
            FireBullet();
        }
    }
// ********************************************************************************************************************************

}
