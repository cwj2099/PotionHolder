using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ResourceManager : MonoBehaviour
{
    public int[] holdingResources;//0 fire, 1 wind, 2 ice
    public int[] selectedResources;//the one player selected to be used
    public TextMeshProUGUI[] selectedNumbers;
    public Button create;
    void Start()
    {
        selectedResources = new int[holdingResources.Length];
    }


    void Update()
    {
        UpdateUI();
    }

    //Call this to add one spefici element in Tower creation
    public void AddOneUnit(int id)
    {
        if (holdingResources[id] > selectedResources[id])
        {
            selectedResources[id]++;
            UpdateUI();
        }
    }

    //Call this to remove one speficied element in Tower creation
    public void ReduceOneUnit(int id)
    {
        if (selectedResources[id] > 0)
        {
            selectedResources[id]--;
            UpdateUI();
        }
    }

    public int[] CreateTower()
    {
        int[] toReturn = selectedResources;
        for (int i = 0; i < holdingResources.Length; i++)
        {
            holdingResources[i] -= selectedResources[i];
        }
        selectedResources = new int[toReturn.Length];

        UpdateUI();
        return toReturn;
    }

    void UpdateUI()
    {
        for(int i = 0; i < holdingResources.Length; i++)
        {
            selectedNumbers[i].text = selectedResources[i] + "/" + holdingResources[i];
        }

        create.gameObject.SetActive(selectedResources.Max() > 0);

    }

}
