using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeCanvas : MonoBehaviour
{
    public GameObject upgradeCanvas;
    public static UpgradeCanvas instance;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    public Button upgradeButton;


    private void Awake()
    {
        upgradeCanvas.SetActive(false);
        instance = this;
    }

    private void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    private void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            if(results.Count==1 && results[0].gameObject.name == "Panel")
            {
                HideCanvas(); //if clicked on the empty area of the panel, hide the canvas.
            }
           
            
        }
    }



    public void ShowCanvas(Vector3 pos)
    {
        upgradeCanvas.SetActive(true);
    
        SetCanvasPosition(pos);

        if (TurretSpawner.instance.cubes[Raycast.currHitCube].tag == "Upgraded")
        {
          
            upgradeButton.interactable = false;
        }
        else
        {
            upgradeButton.interactable = true;
        }
    }

    public void HideCanvas()
    {
        upgradeCanvas.SetActive(false);
        
    }

    public void SetCanvasPosition(Vector3 pos)
    {
        Vector3 offset = new Vector3(0, 6.5f, 0);
        transform.position = pos+offset;
    }
}
