using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* *********************************************************************
 * DEVELOPER: Noah Young
 * DATE: 11/10/2022
 * LAST UPDATED: 11/10/2022
 * PURPOSE: Handles UI for enemies, such as health bars.
 * *********************************************************************
 */
public class EnemyUI : MonoBehaviour
{
    
    private Enemy _enemy; 
    public Enemy Enemy
    {
        get { return _enemy; }
        set { _enemy = value; }
    }

    [SerializeField]
    private Color[] _colors;
    public Color[] Colors
    {
        get { return _colors; }
        set { _colors = value; }
    }

    private List<EnemyHealthBar> _healthbars; 

    private int _hp;

    private int _armor; 

    [SerializeField]
    private GameObject _healthBarPrefab;

    private Transform _healthBarBase;

    private int _currentHealthbarIndex = 0;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        _healthbars = new List<EnemyHealthBar>();
        _healthBarBase = GetComponentInChildren<Transform>();
        EnemyCreateHealthBars();
    }

    /// <summary>
    /// Called on object creation; creates health bars according to the HP of the enemy.
    /// </summary>
    private void EnemyCreateHealthBars()
    {
        Debug.Log("Creating health bars...");

        _hp = _enemy.Hp;
        _armor = _enemy.Armor;

        int _trackHp = _hp;

        for (var i = 0; i <= ((float)_hp / 20); i++)
        {
            int _healthbarHp = Mathf.Min(_trackHp, 20);

            GameObject _healthbarObject = Instantiate(_healthBarPrefab, _healthBarBase);
            EnemyHealthBar _healthbar = _healthbarObject.GetComponent<EnemyHealthBar>();
            
            _healthbars.Add(_healthbar);
            _healthbar.Image.color = _colors[i];
            _healthbar.SetHealth(_healthbarHp);

            _currentHealthbarIndex = i;
        }
    }

    public void UpdateUI(int _damage)
    {
        //  STEP 1: Get the current health bar we're messing with.
        var _currentHealthBar = _healthbars[_currentHealthbarIndex].GetComponent<EnemyHealthBar>();

        //  STEP 2: Check if the damage we're taking will phase us to another health bar.
        if (_currentHealthBar.Health < _damage)
        {
            // If so, reduce the current health bar to nothing...
            _currentHealthBar.SetHealth(0);

            //  And if there's another health bar left...
            if (_currentHealthbarIndex > 0)
            {
                //  Carry the damage over to the next one.
                var _carryoverDamage = _damage - _currentHealthBar.Health;

                _currentHealthbarIndex--;
                _currentHealthBar = _healthbars[_currentHealthbarIndex].GetComponent<EnemyHealthBar>();
                _currentHealthBar.SetHealth(_currentHealthBar.Health - _carryoverDamage);

                return;
            }
        }

        //  STEP 3: Otherwise, just reduce the current health bar. 
        else
        {
            _currentHealthBar.SetHealth(_currentHealthBar.Health - _damage);
        }

    }
}
