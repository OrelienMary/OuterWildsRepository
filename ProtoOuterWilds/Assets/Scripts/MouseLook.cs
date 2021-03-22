using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public static MouseLook ml;

    [HideInInspector] public float mouseX;
    [HideInInspector] public float mouseY;

    public float mouseSensitivity;

    public Transform pCamera;
    public Transform playerRotationGO;

    [HideInInspector] public float cameraRotation;
    [HideInInspector] public float localRotation;

    public bool changing;

    public bool currentPlanetWasNull;

    public float changingDegreeForce;

    // Start is called before the first frame update
    void Awake()
    {
        ml = this;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        cameraRotation -= mouseY;
        localRotation += mouseX;

        if(currentPlanetWasNull == false && PlayerMovement.pm.currentPlanet == null)
        {
            ChangeViewPlanetToSpace();
        }
        else if(currentPlanetWasNull == true && PlayerMovement.pm.currentPlanet != null)
        {
            StartCoroutine(ChangeViewSpaceToPlanet());
        }

        if(changing == false)

        if (PlayerMovement.pm.currentPlanet != null)
        {
            pCamera.localRotation = Quaternion.Euler(cameraRotation, 0f, 0f);

            cameraRotation = Mathf.Clamp(cameraRotation, -80f, 80f);
            playerRotationGO.localRotation = Quaternion.Euler(0f, localRotation, 0f);
        }
        else
        {
            pCamera.localRotation = Quaternion.Euler(Vector3.zero);

            playerRotationGO.localRotation = Quaternion.Euler(0f, 0f, 0f); 
        }
        
        if(PlayerMovement.pm.currentPlanet == null)
        {
            currentPlanetWasNull = true;
        }
        else
        {
            currentPlanetWasNull = false;
        }

        //Vector3.RotateTowards()
    }

    void ChangeViewPlanetToSpace()
    {
        transform.rotation = Quaternion.LookRotation(pCamera.forward, pCamera.up);

        pCamera.rotation = Quaternion.Euler(Vector3.zero);
        playerRotationGO.localRotation = Quaternion.Euler(Vector3.zero);
    }

    IEnumerator ChangeViewSpaceToPlanet()
    {
        changing = true;

        for(float i = 0; i < 0.5f; i += Time.fixedDeltaTime)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.Cross(PlayerMovement.pm.gravityDirection, transform.right), -PlayerMovement.pm.gravityDirection), 10f * Time.fixedDeltaTime);

            //pCamera.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(cameraRotation, 0f, 0f), 10f * Time.fixedDeltaTime);
            //playerRotationGO.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0f, localRotation, 0f), 10f * Time.fixedDeltaTime);

            yield return new WaitForFixedUpdate();
        }

        /*playerRotationGO.localRotation = Quaternion.Euler(new Vector3(0f, pCamera.localRotation.eulerAngles.y, 0f));
        pCamera.localRotation = Quaternion.Euler(new Vector3(pCamera.localRotation.eulerAngles.x, 0f, 0f));*/

        changing = false;
    }
}
