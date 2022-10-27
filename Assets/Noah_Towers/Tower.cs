using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* *********************************************************************
 * DEVELOPER: Noah Young
 * DATE: 10/24/2022
 * LAST UPDATED: 10/27/2022
 * PURPOSE: Base class for all towers; stores all appropriate variables.
 * *********************************************************************
 */


// Enum the element types are stored in; can be modified as necessary. 
// Should probably be moved to the PotionHandler class once that exists. 
public enum Elements
{
    Standard,
    Fire,
    Water,
    Thunder,
    Earth,
}

public abstract class Tower : MonoBehaviour
{
// ********************************************************************************************************************************


    // VARIABLES


    [Header("Prefabs")]
    [Tooltip("The bullet prefab.")] [SerializeField]
    /* BULLET PREFAB 
     * - Necessary to spawn bullet. 
     * */
    private GameObject _bulletPrefab;    //  Prefab for bullets. 

    [Space(10)]

    [Header("Attack Properties")]
    [Tooltip("Strength of the bullet. Default value is 1.")] [SerializeField]
    /* POWER
     * - The power of each bullet fired by this tower. 
     */
    private int _power = 1;
    public int Power
    {
        get { return _power; }
        set { _power = value; }
    }

    [Tooltip("Speed of the bullet. Default value is 1.")] [SerializeField]
    /* SPEED
     * - The speed of each bullet fired by this tower. 
     */
    private float _speed = 1;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    [Tooltip("Size of the bullets. Default value is 1.")] [SerializeField]
    /* SIZE
     * - Size scalar for each bullet fired by this tower.
     */
    private float _size = 1;       
    public float Size
    {
        get { return _size; }
        set { _size = value; }
    }

    [Tooltip("The number of enemies a bullet can tear through before being destroyed. Minimum 1. Default value is 1.")]
    [SerializeField]
    /* PIERCE
     * - The bullet can hurt an enemy for every point of pierce it has. 
     */
    private int _pierce = 1;
    public int Pierce
    {
        get { return _pierce; }
        set { _pierce = value; }
    }

    [Tooltip("Lifespan of the bullets. Default value is 3 (seconds)")]
    [SerializeField]
    /* LIFESPAN
     * - How long a bullet will last before it deletes itself automatically. Used to prevent bullets from going on for infinity, but can be lowered dramatically to create a short range shotgun effect.
     */
    private float _lifespan = 3;
    public float Lifespan
    {
        get { return _lifespan; }
        set { _lifespan = value; }
    }

    [Space(5)]

    [Tooltip("Rate of fire; tower fires a bullet every N seconds. Default value is 1.")] [SerializeField]
    /* RATE
     * - The tower's rate of fire. The higher it is, the faster it shoots.
     */
    private float _rate = 1;
    public float Rate
    {
        get { return _rate; }
        set { _rate = value; }
    }


    [Tooltip("How intense the bullet spread is. Default value is 0.")] [SerializeField]
    /* SPREAD
     * - Determines how much bullet spread the tower has. The more spread, the more likely it is a given bullet will miss its target.
     */
    private float _spread = 0;
    public float Spread
    {
        get { return _spread; }
        set { _spread = value; }
    }

    [Space(10)]
// ********************************************************************************************************************************


    //  ELEMENT


    [Header("Element")]
    [Tooltip("The base element of the tower, to be set on instantiation. Default value is Standard.")] 
    private Elements _element = Elements.Standard;
    public Elements Element
    {
        get { return _element; }
        set { _element = value; }
    }

// ********************************************************************************************************************************


    // FUNCTIONS

    /// <summary>
    /// FireBullet() passes all relevant info to a new bullet and instantiates it.
    /// </summary>
    protected void FireBullet()
    {
        //  Create new bullet. 
        TowerBullet bullet = _bulletPrefab.GetComponent<TowerBullet>();

        //  Assign stats of bullet.
        bullet.Power = _power;
        bullet.Speed = _speed;
        bullet.BaseElement = _element;
        bullet.Pierce = _pierce;
        bullet.transform.localScale = new Vector3(_size, _size, _size);

        //  Calculate spread.
        var _randomSpread = Random.Range(-_spread, _spread);

        //  Point bullet in right direction. 
        bullet.transform.forward = transform.forward + new Vector3(_randomSpread, 0, _randomSpread);

        //  Fire.
        Instantiate(_bulletPrefab, transform.position, transform.rotation);
    }
}
