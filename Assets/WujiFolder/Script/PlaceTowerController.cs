using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlaceTowerController : MonoBehaviour
{
    bool isLocating=false;
    public Camera mainCamera;
    public string targetTag="Placement";
    public LayerMask rayMask;
    public GameObject tower;//the tower prefab
    public GameObject currentTower;//the currentTower to be handled
    public GameObject occupyPlacement;
    public Vector3 placementOffset;
    public UnityEvent onSuccessPlacement;

    Color oColor;
    // Start is called before the first frame update
    void Start()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocating)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out hit, Mathf.Infinity, rayMask))
            {
                Transform objectHit = hit.transform;
                //Debug.Log("hitting " + objectHit);
                //Debug.Log("hitting at " + hit.point);
                if (currentTower != null)
                {
                    //move with mouse
                    currentTower.transform.position = hit.point+placementOffset;
                    
                    //handle not able to place apperance
                    if (objectHit.tag != targetTag)
                    {
                        if (currentTower.GetComponent<MeshRenderer>() != null)
                        { currentTower.GetComponent<MeshRenderer>().material.color = new Color(255,0,0); }
                    }
                    else
                    {
                        if (currentTower.GetComponent<MeshRenderer>() != null)
                        { currentTower.GetComponent<MeshRenderer>().material.color = oColor; }
                    }


                    //handle successful placement
                    if (objectHit.tag==targetTag&&Input.GetKey(KeyCode.Mouse0))
                    {
                        PlaceTower(hit.point);
                    }
                }
          
            }
        }
    }

    public void OnEnable()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        //create object and start location calculation
        isLocating = true;
        currentTower=Instantiate(tower);

        //Set animation mode
        if (currentTower.GetComponent<Animator>() != null) 
        { currentTower.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime; }

        //save original color
        if (currentTower.GetComponent<MeshRenderer>() != null)
        { oColor = currentTower.GetComponent<MeshRenderer>().material.color; }
    }

    public void OnDisable()
    {
        isLocating = false;

        //force placement on disable
        if (currentTower != null)
        {
            PlaceTower(currentTower.transform.position - placementOffset);
        }
    }

    public void ChangeTower(GameObject t)
    {
        tower = t;
    }

    public void PlaceTower(Vector3 point)
    {
        //Create the occupier
        Instantiate(occupyPlacement, point, Quaternion.identity);

        //reset animation mode
        if (currentTower.GetComponent<Animator>() != null)
        { currentTower.GetComponent<Animator>().updateMode = AnimatorUpdateMode.Normal; }

        onSuccessPlacement.Invoke();
    }
}
