using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float mouseX;
    float mouseY;

    public float mouseSensitivity;

    public Transform pCamera;
    public Transform playerRotationGO;

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
        localRotation += mouseX;

        if (PlayerMovement.pm.currentPlanet != null)
        {
            pCamera.localRotation = Quaternion.Euler(cameraRotation, 0f, 0f);
            playerRotationGO.parent.localRotation = Quaternion.Euler(Vector3.zero);

            cameraRotation = Mathf.Clamp(cameraRotation, -80f, 80f);
            playerRotationGO.localRotation = Quaternion.Euler(0f, localRotation, 0f);
        }
        else
        {
            pCamera.localRotation = Quaternion.Euler(Vector3.zero);
            playerRotationGO.parent.localRotation = Quaternion.Euler(cameraRotation, 0f, 0f);

            playerRotationGO.localRotation = Quaternion.Euler(0f, localRotation, 0f); 
        }
        
        //Vector3.RotateTowards()
    }
}
