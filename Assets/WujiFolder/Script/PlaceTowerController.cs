using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public TowerLooking currentLooking;
    public GameObject occupyPlacement;
    public Vector3 placementOffset;
    public UnityEvent onSuccessPlacement;
    public ResourceManager resourceManager;

    Color oColor;
    // Start is called before the first frame update
    void Start()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if(resourceManager == null)
        {
            resourceManager = FindObjectOfType<ResourceManager>();  
        }
    }

    /**
     * You move the tower around 
     * And update the looking between placable & not placable
     * 
     */
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

    /**
     * Upon enable, you will instantiate a tower
     */
    public void OnEnable()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        //get resource from resource manager
        int[] data = resourceManager.CreateTower();
        

        //create object and start location calculation
        isLocating = true;
        currentTower=Instantiate(tower);


        //Update the looking accordingly
        currentLooking = currentTower.GetComponent<TowerLooking>();
        if (currentLooking == null) { currentLooking = currentTower.GetComponentInChildren<TowerLooking>(); }
        if (currentLooking != null) { currentLooking.SetUpLooking(data); }


        //Update Tower Attributes accordingly
        Tower t = currentTower.GetComponent<Tower>();
        if (t == null) { t = currentTower.GetComponentInChildren<Tower>(); }

        //The Greatest number decide the power & size
        t.enabled = false;
        t.Power= data.Max()*5; t.Size = data.Max()/5f; t.GetComponent<TowerAutoAlter>().Range = 1 + data.Max();
        currentTower.transform.localScale = new Vector3(0.5f + data.Max() * 0.2f, 0.5f + data.Max() * 0.2f, 0.5f + data.Max() * 0.2f);

        //The fire decide the piercing
        t.Pierce = 1 + data[0];
        //The wind decide the fire rate
        t.Rate = 1 - (0.1f * data[1]);
        //The ice decide the speed
        t.Speed = 0.25f + data[2]/4f;
        //The earth decide the life
        t.Lifespan = 1+ data[3];
        //set looking
        t.Element = (Elements)data.ToList().IndexOf(data.Max());

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

    /**
     * Change the tower prefab, if needed
     */
    public void ChangeTower(GameObject t)
    {
        tower = t;
    }

    /**
     * Events to execute upon tower placements
     */
    public void PlaceTower(Vector3 point)
    {
        //Create the occupier
        Instantiate(occupyPlacement, point, Quaternion.identity);

        //reset animation mode
        if (currentTower.GetComponent<Animator>() != null)
        { currentTower.GetComponent<Animator>().updateMode = AnimatorUpdateMode.Normal; }
        currentLooking = null;
        onSuccessPlacement.Invoke();

        Tower t = currentTower.GetComponent<Tower>();
        if (t == null) { t = currentTower.GetComponentInChildren<Tower>(); }

        
        t.enabled = true;
    }
}
