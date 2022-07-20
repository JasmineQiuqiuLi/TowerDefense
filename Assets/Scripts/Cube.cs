using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    Material material;
    private void Awake()
    {
        material = gameObject.GetComponent<Renderer>().material;
    }
    private void OnMouseOver()
    {
        material.color = Color.red;
    }

    private void OnMouseExit()
    {
        material.color = Color.white;
    }
}
