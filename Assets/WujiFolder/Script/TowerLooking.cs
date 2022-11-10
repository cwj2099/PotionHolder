using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TowerLooking : MonoBehaviour
{
    public Material[] TowerMaterials;//fire 1, wind 2, ice 3
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer aimerRenderer;
    public Transform aimer;
    public GameObject range;
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
    }

    public void RotateAimer(Vector3 r)
    {
        aimer.Rotate(r);
    }
}
