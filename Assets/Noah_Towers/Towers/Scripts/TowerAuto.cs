using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* *********************************************************************
 * DEVELOPER: Noah Young
 * DATE: 10/27/2022
 * LAST UPDATED: 10/29/2022
 * PURPOSE: Tower class for all non-player towers. This is the prefab that should be spawned when crafting. 
 * *********************************************************************
 */
public class TowerAuto : Tower
{
    //                  IMPORTANT NOTE
    //  This class inherits from the Tower class, whose variables are almost all protected.
    //  This means that this class will inheirit all of those variables, even if they're not listed here.
    //  When instantiating an automatic tower via crafting, remember you can access the private versions of variables (ie. _power, _speed, _rate, etc.) *from this class*. 


    public enum TargetSetting
    {
        Nearest,    // Target the enemy with the least travel distance. 
        Farthest,   // Target the enemy with the most travel distance. 
        Closest,    // Target the enemy that is physically closest to the tower. 
        Strongest   // Target the enemy with the greatest Toughness value. 
    }

    // Tells the tower how to prioritize enemies within its range. The default value is Nearest, see above for details. 
    private TargetSetting _currentTargetSetting = TargetSetting.Nearest;
    public TargetSetting CurrentTargetSetting
    {
        get { return _currentTargetSetting; }
        set { _currentTargetSetting = value; }
    }

    //  Keeps track of the enemies within the range of the tower. 
    private List<Enemy> _enemies;

    //  The range of the tower, to be set upon crafting. Default is 2;
    private float _range = 2f;
    public float Range
    {
        get { return _range; }
        set { _range = value; }
    }

    // ********************************************************************************************************************************

    /// <summary>
    /// CreateRange() creates a sphere collider around each Tower based on the range. 
    /// </summary>
    protected void CreateRange()
    {
        SphereCollider _sphereCollider = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;

        _sphereCollider.radius = _range;
        _sphereCollider.isTrigger = true;
    }

    /// <summary>
    /// TargetEnemy() rotates the tower to face an enemy within its range depending on the tower's Target Setting. 
    /// </summary>
    protected void TargetEnemy()
    {
        //  If there are no enemies in range, skip any further calculations.
        if (_enemies.Count == 0)
        {
            return;
        }


        switch (_currentTargetSetting)
        {
            //  Find the enemy with the lowest travel distance and target that enemy.
            case (TargetSetting.Nearest):

                Enemy _nearestEnemy = _enemies[0];

                for (var i = 0; i < _enemies.Count; i++)
                {
                    Enemy _currentEnemy = _enemies[i];
                    if (_currentEnemy.DistanceTraveled < _nearestEnemy.DistanceTraveled)
                    {
                        _nearestEnemy = _enemies[i];
                    }
                }

                transform.LookAt(_nearestEnemy.transform);

                break;

            /* TO DO: Add functionality for other Target Settings. */
        }
    }

    /// <summary>
    /// AutoFire() will tell the Tower to fire at the assigned rate of fire. The couroutine's speed depends on the _rate of the Tower. 
    /// </summary>
    /// <param name="rate">The rate of fire.</param>
    protected IEnumerator AutoFire(float rate)
    {
        while (true)
        {
            yield return new WaitForSeconds(rate);
            Debug.Log("Fired!");
            TargetEnemy();
            FireBullet();
        }
    }
    // ********************************************************************************************************************************

    private void Start()
    {
        _enemies = new List<Enemy>();

        CreateRange();
        StartCoroutine(AutoFire(_rate));
    }

    //  Add enemies when entering the Tower's radius,
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy within range.");
            _enemies.Add(other.GetComponent<Enemy>());
        }
    }

    //  And remove them when exiting.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemies.Remove(other.GetComponent<Enemy>());
        }
    }
}
