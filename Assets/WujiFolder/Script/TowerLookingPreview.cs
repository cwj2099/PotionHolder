using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TowerLookingPreview : MonoBehaviour
{
    public GameObject button;
    public Material[] TowerMaterials;//fire 1, wind 2, ice 3
    public Image bodyRenderer;
    public Image aimerRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpLooking(int[] elements)
    {
        int maxValue = elements.Max();
        int maxIndex = elements.ToList().IndexOf(maxValue);
        bodyRenderer.material = TowerMaterials[maxIndex];
        aimerRenderer.material = TowerMaterials[maxIndex];
        button.transform.localScale = new Vector3(0.5f + elements.Max() * 0.2f, 0.5f + elements.Max() * 0.2f, 0.5f + elements.Max() * 0.2f);
    }
}
