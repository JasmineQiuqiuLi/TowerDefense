using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Raycast : MonoBehaviour
{
    RaycastHit hit;
    public static Material onOverMaterial;
    private Renderer preRender;
    public Material preMaterial;
    public static GameObject currHitCube;

    private void Update()
    {   
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if(preRender!=null) preRender.sharedMaterial = preMaterial;
            //Debug.Log(hit.collider.gameObject.name);
            if (EventSystem.current.IsPointerOverGameObject()==false && hit.collider.CompareTag("Cube"))
            {
                currHitCube = hit.collider.gameObject;
                Renderer render = hit.collider.gameObject.GetComponent<Renderer>();
                render.material = onOverMaterial;
                preRender = render;
                if (Input.GetMouseButtonDown(0))
                {
                    TurretSpawner.instance.SpawnTurret(currHitCube);
                }
            }
        }
    }
}
