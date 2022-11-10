using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddElementOnDeath : MonoBehaviour
{
    public int[] resourcesChance;//out of ten
    public GameObject[] effects;
    private void OnDisable()
    {
        ResourceManager r=FindObjectOfType<ResourceManager>();
        if (r != null)
        {
            for(int i=0;i<resourcesChance.Length;i++)
            { 
                if (Random.Range(1, 10) <= resourcesChance[i])
                {
                    var temp=Instantiate(effects[i]);
                    temp.transform.position = transform.position;
                    r.holdingResources[i]++;
                }
            }
        }
    }
}
