using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* *********************************************************************
 * DEVELOPER: Noah Young
 * DATE: 10/24/2022
 * LAST UPDATED: 10/24/2022
 * PURPOSE: Base class for bullets. They basically just move forward for now. 
 * *********************************************************************
 */
public class TowerBullet : MonoBehaviour
{
    public float power;
    public float speed;
    public float size;

    private float timeToDeletion = 3f;

    //TO-DO: public Elements element;

    private void Start()
    {
        //  Time-out code.
        StartCoroutine(BulletTimeout(timeToDeletion));
    }

    private void Update()
    {
        transform.position += transform.forward * speed;
    }

    //  Prevents bullets from staying on-screen forever. Will be slightly edited as we finalize map design.
    private IEnumerator BulletTimeout(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
