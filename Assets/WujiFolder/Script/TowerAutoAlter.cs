using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//copied from original
public class TowerAutoAlter:Tower
{
    public Enemy _nearestEnemy;
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
    [SerializeField]
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

                List<int> toRemove= new List<int>();

                _nearestEnemy = _enemies[0];

                for (var i = 0; i < _enemies.Count; i++)
                {                   
                   
                    Enemy _currentEnemy = _enemies[i];
                    if (_currentEnemy.DistanceTraveled < _nearestEnemy.DistanceTraveled)
                    {
                        _nearestEnemy = _enemies[i];
                    }

                    if (!_currentEnemy.gameObject.activeInHierarchy) { toRemove.Add(i); }
                }

                //add this to avoid targeting on dead enemies
                for (var i = 0; i < _enemies.Count; i++)
                {
                    if (toRemove.Contains(i))
                    {
                        _enemies.RemoveAt(i);
                    }
                }

                transform.LookAt(_nearestEnemy.transform);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

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
            FireAlter();
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
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy within range.");
            if (!_enemies.Contains(other.GetComponent<Enemy>()))
            {
                _enemies.Add(other.GetComponent<Enemy>());
            }
        }
    }

    //  And remove them when exiting.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy has exited range.");
            _enemies.Remove(other.GetComponent<Enemy>());
        }
    }

    public void FireAlter()
    {
        var _bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        TowerBulletAlter _towerBullet = _bullet.GetComponent<TowerBulletAlter>();

        //  Assign stats of bullet.
        _towerBullet.Power = _power;
        _towerBullet.Speed = _speed;
        _towerBullet.BaseElement = _element;
        _towerBullet.Pierce = _pierce;
        _towerBullet.Lifespan = _lifespan;

        //  Assign locational position of bullet. 
        _bullet.transform.localScale = new Vector3(_size, _size, _size);

        //  Calculate spread.
        var _randomSpread = Random.Range(-_spread, _spread);

        //  Point bullet in right direction. 
        _bullet.transform.forward = transform.forward + new Vector3(_randomSpread, 0, _randomSpread);
        _towerBullet.BaseElement = Element;
    }
}
