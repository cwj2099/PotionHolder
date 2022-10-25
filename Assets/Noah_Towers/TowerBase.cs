using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* *********************************************************************
 * DEVELOPER: Noah Young
 * DATE: 10/24/2022
 * PURPOSE: Base class for all towers; stores all appropriate variables.
 * *********************************************************************
 */


// Enum the element types are stored in; can be modified as necessary. 
// Should probably be moved to the PotionHandler class once that exists. 
public enum Elements
{
    Standard,
    Fire,
    Ice,
    Electric,
    Earth
}

public class TowerBase : MonoBehaviour
{

    [Header("Prefabs")]
    public GameObject bulletPrefab;    // Prefab for bullets.

    [Space(10)]
    [Header("Attack Properties")]
    public float attackPower;       //  Strength of the attack. 
    public float attackSpeed;       //  Projectile speed.
    public float attackRate;        //  Rate of fire.
    public float attackSize;        //  Relative size of the attack. 
    public float attackSpread;      //  Attack spread. 

    [Space(10)]
    [Header("Elemental Ability")]
    public Elements element = Elements.Standard;    //  Default Element is standard, should be overwritten on instantiation in PotionHandler once implemented.

    [Space(10)]
    [Header("Rotation")]
    public float angleSpeed;        //  Speed at which the tower will rotate in relation to the mouse. 
    public float angle;            //  The current angle of the tower. 


    //  Fire bullet with proper parameters.
    //  ***TO-DO*** Implement attributes into each bullet. Perhaps change color? 
    public void FireBullet()
    {
        TowerBullet bullet = bulletPrefab.GetComponent<TowerBullet>();

        bullet.power = attackPower;
        bullet.speed = attackSpeed;
        bullet.transform.localScale = new Vector3(attackSize, attackSize, attackSize);

        var spread = Random.Range(-attackSpread, attackSpread);

        bullet.transform.forward = transform.forward + new Vector3(spread, 0, spread);

        Instantiate(bulletPrefab);
    }

    //  Coroutine that can be called to automatically fire bullets based on their rate of fire. 
    private IEnumerator AutoFire(float rate)
    {
        while (true)
        {
            yield return new WaitForSeconds(rate);
            FireBullet();
        }
    }

}
