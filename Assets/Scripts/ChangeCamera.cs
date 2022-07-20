using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    private float vertical;
    private float horizontal;
    private float speed = 10;

    private float minFov = 15;
    private float maxFov = 90;
    private float sensitivity = 10;
    private float fov;


    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
        transform.position += movement;

        fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }


}
