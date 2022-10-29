using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* *********************************************************************
 * DEVELOPER: Noah Young
 * DATE: 10/27/2022
 * LAST UPDATED: 10/29/2022
 * PURPOSE: Base class designed for quick iteration of enemy types. 
 * *********************************************************************
 */

public abstract class Enemy : MonoBehaviour
{
// ********************************************************************************************************************************


    //  HEALTH


    [Header("Health")]
    [Tooltip("How much HP the enemy starts with. Default value is 100.")] [SerializeField]
    /* HP
     * - Self-explanatory. The amount of HP the enemy starts with. 
     */
    private int _hp = 100;
    public int Hp
    {
        get { return _hp; }
        set { _hp = value; }
    }

    /* MAX HP
     * - Set in Start() to equal _hp, exists to avoid overhealing. 
     */
    private int _maxHp; 

    [Tooltip("How much Armor the enemy starts with. Default value is 0.")] [SerializeField]
    /* ARMOR 
     * - Can be added in addition to health. As long as the enemy has armor remaining, damage will be calculated using it instead of HP. 
     * - Damage calculated using armor is automatically reduced by 50%, rounded up. Toughness (see below) does not affect armor. 
     */
    private int _armor = 0;
    public int Armor
    {
        get { return _armor; }
        set { _armor = value; }
    }

    [Tooltip("Damage reduction based on percentage. Default value is 0.")] [Range(0, 100)] [SerializeField]
    /* TOUGHNESS
     * - Reduces damage received by the amount; can be used to create tankier enemies or even bosses. 
     */
    private int _toughness = 0;
    public int Toughness
    {
        get { return _toughness; }
        set { _toughness = value; }
    }

// ********************************************************************************************************************************


    // SPEED


    [Space(10)]

    [Header("Speed")]
    [Tooltip("How fast the enemy moves. Default value is 1.")] [SerializeField]
    /* SPEED
     * - Self-explanatory. Modfiies the speed at which the enemy moves along the path. 
     */
    private float _speed = 1;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    [System.NonSerialized]
    /* DISTANCE TRAVELED
     * - Changes as the enemy moves; used to calculate which enemy a Tower should target. 
     */
    private float _distanceTraveled;
    public float DistanceTraveled
    {
        get { return _distanceTraveled; }
        set { _distanceTraveled = value; }
    }

// ********************************************************************************************************************************


    // PROPERTIES -- ELEMENTS


    [Space(10)]

    [Header("Properties")]
    [Tooltip("Base element of enemy. Default value is Standard.")] [SerializeField]
    /* BASE ELEMENT
     * - The "base element" of an enemy. Used to determine what kind of elemental resource the player gains upon enemy death.
     * */
    private Elements _baseElement;
    public Elements BaseElement
    {
        get { return _baseElement; }
        set { _baseElement = value; }
    }

    [Tooltip("Additional weaknesses of an enemy. By default, the enemy has its elemental weaknesses.")] [SerializeField]
    /* ENEMY WEAKNESSES
     * - Determines what elements the enemy is weak to. When an enemy is weak to an element, they will take 50% more damage from attacks with that element. 
     */
    private List<Elements> _weaknessElements;
    public List<Elements> WeaknessElements
    {
        get { return _weaknessElements; }
        set { _weaknessElements = value; }
    }

    [Tooltip("Additional resistances of an enemy. By default, the enemy has its elemental resistances.")] [SerializeField]
    /* ENEMY RESISTANCES
     * - Determines what elements the enemy resists. When an enemy resists an element, they will take 50% less damage from attacks with that element. 
     */
    private List<Elements> _resistanceElements;
    public List<Elements> ResistanceElements
    {
        get { return _resistanceElements; }
        set { _resistanceElements = value; }
    }

    [Tooltip("Immunities of an enemy. By default, the enemy has no immunities.")]
    [SerializeField]
    /* ENEMY IMMUNITIES
     * - Determines what elements the enemy is immune to. When an enemy is immune an element, they will take no damage from attacks with that element. 
     */
    private List<Elements> _immunityElements;
    public List<Elements> ImmunityElements
    {
        get { return _immunityElements; }
        set { _immunityElements = value; }
    }

    [Space(5)]
    

    //  PROPERTIES -- MISCELLANEOUS 


    [Tooltip("Determines whether bullets can pierce this enemy. Default value is false.")] [SerializeField]
    /* CANNOT BE PIERCED
     * - Determines if a bullet can pierce this enemy. If true, the bullet will be destroyed on impact regardless of how much piercing power it may have. 
     */
    private bool _cannotBePierced = false;
    public bool CannotBePierced
    {
        get { return _cannotBePierced; }
        set { _cannotBePierced = value; }
    }

    [Tooltip("Determines whether or not an enemy should skip the auto-setting process. Default value is false.")] [SerializeField]
    /* DON'T SET ELEMENT PROPERTIES
     * - By default, enemies will automatically add their element's Resistances and Weaknesses based on their base element. If you want an enemy to bypass this behavior, check this box. 
     */
    private bool _dontSetElementProperties = false;

    /* More to be added. */


    // PRIVATE VARIABLES

    //  Called in favor of "transform"; saves resources, I promise. 
    private Transform _tr;

    //  Vec3 used in distance calculations.
    private Vector3 _oldPos; 


// ********************************************************************************************************************************


    // FUNCTIONS

    /// <summary>
    /// EnemyInit() is a function that will be run whenever an enemy is spawned. 
    /// By default, it does nothing, but if you want a certain enemy type to execute exclusive code on instantiation, you can derive a class for it and override this function. 
    /// </summary>
    public virtual void EnemyInit(){ 
        
    }

    /// <summary>
    /// EnemyOnHit() is a function that will be run whenever an enemy is hit.
    /// By default, it does nothing, but if you want a certain enemy type to execute exclusive code on hit, you can derive a class for it and override this function. 
    /// </summary>
    public virtual void EnemyOnHit()
    {

    }

    /// <summary>
    /// EnemyOnDestroy() is a function that will be run whenever an enemy is destroyed.
    /// By default, it does nothing, but if you want a certain enemy type to execute exclusive code on destruction, you can derive a class for it and override this function. 
    /// </summary>
    public virtual void EnemyOnDestroy()
    {

    }

    /// <summary>
    /// SetDefaultEnemyProperties() looks at the Enemy's base element and assigns resistances and weaknesses accordingly. 
    /// </summary>
    private void SetDefaultElementProperties()
    {
        switch (_baseElement)
        {
            case (Elements.Standard):
                break;

            case (Elements.Fire):
                //  Weak to: WATER
                //  Resists: EARTH   
                _weaknessElements.Add(Elements.Water);
                _resistanceElements.Add(Elements.Earth);
                break;

            case (Elements.Water):
                //  Weak to: THUNDER
                //  Resists: FIRE   
                _weaknessElements.Add(Elements.Thunder);
                _resistanceElements.Add(Elements.Fire);
                break;

            case (Elements.Earth):
                //  Weak to: FIRE
                //  Resists: THUNDER
                _weaknessElements.Add(Elements.Fire);
                _resistanceElements.Add(Elements.Thunder);
                break;

            case (Elements.Thunder):
                //  Weak to: EARTH
                //  Resists: WATER
                _weaknessElements.Add(Elements.Earth);
                _resistanceElements.Add(Elements.Water);
                break;
        }
    }

    /// <summary>
    /// TrackDistance() tracks the distance the enemy has moved and writes the value to _distanceTraveled. Used in tower-targeting calculations. 
    /// </summary>
    private void TrackDistance()
    {
        Vector3 _distanceVector = _tr.position - _oldPos;
        float _distanceThisFrame = _distanceVector.magnitude;
        _distanceTraveled += _distanceThisFrame;
        _oldPos = _tr.position;
    }

// ********************************************************************************************************************************


    // BEHAVIOR


    private void Start()
    {
        EnemyInit(); 

        //  Will run by default. 
        if (!_dontSetElementProperties) 
        {
            SetDefaultElementProperties();
        }
 
        _maxHp = _hp;

        _tr = transform;
        _oldPos = _tr.position;
    }

    private void Update()
    {
        TrackDistance();
    }
}
