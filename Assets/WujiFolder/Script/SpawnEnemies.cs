using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct SpawnData
{
    public int amount;
    public int type;
    public int route;
    public bool randomOffset;
    public Vector3 offset;
    public bool facingRight;
    public float time;
}
public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] EnemyPool;
    public GameObject[] EnemyRoutes;

    public SpawnData[] SpawnDatas;
    // Start is called before the first frame update




    IEnumerator SpawnLoop(int i)
    {
        Debug.Log("now at "+ i);
        if (i>=SpawnDatas.Length)
        {
            yield return null;
        }
        else
        {
            for(int j = 0; j < SpawnDatas[i].amount; j++)
            {
                Debug.Log(j+"out of"+ SpawnDatas[i].amount+" of "+i);
                int temp = 1;
                if (SpawnDatas[i].facingRight) { temp = -1; }

                Vector3 offset = SpawnDatas[i].offset;
                if (SpawnDatas[i].randomOffset) { offset= new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)); }
                SpawnEnemy(SpawnDatas[i].type, SpawnDatas[i].route, offset, temp);
            }
            yield return new WaitForSeconds(SpawnDatas[i].time);
            StartCoroutine(SpawnLoop(i + 1));
        }
        
    }

    public void SpawnEnemy(int type, int route,Vector3 offset, int facing)
    {
        
        GameObject path = Instantiate(EnemyRoutes[route]);
        GameObject enemy = Instantiate(EnemyPool[type]);
        enemy.transform.parent = path.transform;

        enemy.transform.localPosition = offset;//
        enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * facing, enemy.transform.localScale.y, enemy.transform.localScale.z);
        enemy.GetComponent<Enemy>()._parent = path;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(SpawnLoop(0));
    }

}
