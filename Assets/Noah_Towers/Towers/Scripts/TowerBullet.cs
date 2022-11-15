using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* *********************************************************************
 * DEVELOPER: Noah Young
 * DATE: 10/24/2022
 * LAST UPDATED: 10/27/2022
 * PURPOSE: Base class for bullets. Properties are passed by towers.
 * *********************************************************************
 */
public class TowerBullet : MonoBehaviour
{
    private float _power;
    public float Power
    {
        get { return _power; }
        set { _power = value; }
    }

    private float _speed;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    private float _size;
    public float Size
    {
        get { return _size; }
        set { _size = value; }
    }

    private float _lifespan;
    public float Lifespan
    {
        get { return _lifespan; }
        set { _lifespan = value; }
    }

    private int _pierce;
    public int Pierce
    {
        get { return _pierce; }
        set { _pierce = value; }
    }

    private Elements _baseElement;
    public Elements BaseElement
    {
        get { return _baseElement; }
        set { _baseElement = value; }
    }

    [Header("Visuals")]
    [SerializeField]
    private GameObject _explosion; 
    // ********************************************************************************************************************************


    /// <summary>
    /// CheckPierce() looks up how much Pierce a bullet has left and either destroys the bullet or tells it to keep going.
    /// </summary>
    /// <param name="enemy">The Enemy class (or any of its children) of the enemy taking damage.</param>
    private void CheckPierce(Enemy enemy)
    {
        if (enemy.CannotBePierced)
        {
            //  TO-DO: Insert bullet destruction instantiation. 
            Destroy(gameObject);
        }

        else
        {
            if (_pierce <= 0)
            {
                Destroy(gameObject);
            }

            else
            {
                _pierce--;
            }
        }
    }

    /// <summary>
    /// Calculates the damage that a given enemy should take, then removes the bullet as necessary. 
    /// </summary>
    /// <param name="_enemy">The Enemy class (or any of its children) of the enemy taking damage.</param>
    private void CalculateDamage(Enemy _enemy)
    {
        float _rawDamage;
        int _damage;
        float _weaknessFactor = 1;
        float _resistanceFactor = 1;
        float _armorFactor = 1;

        bool _hasArmor = false;

        //  STEP 0. Check for immunities. If there are any, calculate pierce and exit immediately. 
        foreach(Elements _enemyElement in _enemy.ImmunityElements)
        {
            if (_baseElement == _enemyElement)
            {
                CheckPierce(_enemy);
                return; 
            }
        }

        //  STEP 1: Now that we know we're actually dealing damage, run EnemyOnHit() before continuing. 
        _enemy.EnemyOnHit();

        //  STEP 2: Apply weaknesses. 
        foreach(Elements _enemyElement in _enemy.WeaknessElements)
        {
            if (_baseElement == _enemyElement)
            {
                _weaknessFactor = _weaknessFactor * 2;
            }
        }

        //  STEP 3: Apply resistances. 
        foreach (Elements _enemyElement in _enemy.ResistanceElements)
        {
            if (_baseElement == _enemyElement)
            {
                _resistanceFactor = _resistanceFactor / 2;
            }
        }

        //  STEP 4: Check for armor.
        if (_enemy.Armor > 0)
        {
            _armorFactor = 0.5f;
            _hasArmor = true;
        }

        //  STEP 5: Calculate damage.
        _rawDamage = (_power * _weaknessFactor * _resistanceFactor * _armorFactor);
        _rawDamage -= _rawDamage * (_enemy.Toughness / 100);
        _damage = (int)(Mathf.Round(_rawDamage));

        //  STEP 6: Apply damage accordingly.
        if (_hasArmor)
        {
            //  If our damage would exceed enemy armor, set armor to 0 and apply the rest of the damage to HP. 
            if (_damage > _enemy.Armor)
            {
                _damage -= _enemy.Armor;
                _enemy.Armor = 0;
                _enemy.Hp -= _damage;
            }

            //  Otherwise, just damage the armor.
            else
            {
                _enemy.Armor -= _damage;
            }
        }

        else
        {
            _enemy.Hp -= _damage;
        }

        //  STEP 7: Check if we should destroy the enemy.
        if (_enemy.Hp <= 0)
        {
            _enemy.EnemyOnDestroy();
            //Destroy(_enemy.gameObject);
        }

        //  STEP 8: Check the pierce to destroy the bullet as necessary.
        CheckPierce(_enemy);
    }

    /// <summary>
    /// MoveBullet() is a function that tells the bullet how to move. 
    /// By default, this function tells the bullet to move forward in a straight line, but if you want custom movement for a certain type of bullet, create a derivative of the TowerBullet class and override this function.
    /// </summary>
    protected virtual void MoveBullet()
    {
        transform.position += transform.forward * _speed;
        Debug.Log(transform.position);
    }

    // ********************************************************************************************************************************

    private void Start()
    {
        Debug.Log(_lifespan);
        Debug.Log(_power);
        //  Time-out code.
        StartCoroutine(BulletTimeout(_lifespan));

    }

    private void Update()
    {
        MoveBullet();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            CalculateDamage(other.GetComponent<Enemy>()); 
        }
    }

    //  Prevents bullets from staying on-screen forever. Will be slightly edited as we finalize map design.
    private IEnumerator BulletTimeout(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }



}
