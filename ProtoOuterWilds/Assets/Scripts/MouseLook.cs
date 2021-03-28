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

    public float changeRotationSpeed = 0.01f;

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
            changing = true;

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

        if (PlayerMovement.pm.currentPlanet == null)
        {
            currentPlanetWasNull = true;
        }
        else
        {
            currentPlanetWasNull = false;
        }
    }

    void ChangeViewPlanetToSpace()
    {
        Debug.Log("Change View Planet To Space");

        transform.rotation = Quaternion.LookRotation(pCamera.forward, pCamera.up);

        pCamera.rotation = Quaternion.Euler(Vector3.zero);
        playerRotationGO.localRotation = Quaternion.Euler(Vector3.zero);
    }

    IEnumerator ChangeViewSpaceToPlanet()
    {
        changing = true;

        Quaternion target = Quaternion.LookRotation(Vector3.Cross(PlayerMovement.pm.gravityDirection, transform.right), -PlayerMovement.pm.gravityDirection);

        while (Quaternion.Angle(transform.rotation, target) > 5f )
        {
            target = Quaternion.LookRotation(Vector3.Cross(PlayerMovement.pm.gravityDirection, transform.right), -PlayerMovement.pm.gravityDirection);

            Debug.Log(Vector3.Cross(PlayerMovement.pm.gravityDirection, transform.right));

            Debug.DrawRay(transform.position, Vector3.Cross(PlayerMovement.pm.gravityDirection, transform.right) * 10f, Color.red, 20f);

            transform.rotation = Quaternion.Slerp(transform.rotation, target, changeRotationSpeed);

            yield return new WaitForFixedUpdate();
        }

        cameraRotation = 0;
        localRotation = 0;

        Debug.Log(Vector3.Cross(PlayerMovement.pm.gravityDirection, transform.right));

        /*playerRotationGO.localRotation = Quaternion.Euler(new Vector3(0f, pCamera.localRotation.eulerAngles.y, 0f));
        pCamera.localRotation = Quaternion.Euler(new Vector3(pCamera.localRotation.eulerAngles.x, 0f, 0f));*/

        changing = false;
    }
}
