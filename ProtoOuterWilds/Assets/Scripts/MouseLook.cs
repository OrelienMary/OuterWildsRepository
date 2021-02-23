using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float mouseX;
    float mouseY;

    public float mouseSensitivity;

    public Transform pCamera;
    float cameraRotation;
    float localRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        cameraRotation -= mouseY;
        cameraRotation = Mathf.Clamp(cameraRotation, -80f, 80f);

        localRotation += mouseX;

        pCamera.localRotation = Quaternion.Euler(cameraRotation, 0f, 0f);

        float xAngle = Vector2.Angle(Vector3.up, new Vector3(transform.position.x, 0f, 0f) - new Vector3(PlayerMovement.pm.currentPlanet.position.x,0f,0f));
        float zAngle = Vector2.Angle(Vector3.up, new Vector3(0f, 0f, transform.position.z) - new Vector3(0f, 0f, PlayerMovement.pm.currentPlanet.position.z));

        transform.localRotation = Quaternion.Euler(0f, localRotation, 0f);
        
    }
}
