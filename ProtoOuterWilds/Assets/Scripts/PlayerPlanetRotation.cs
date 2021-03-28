using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanetRotation : MonoBehaviour
{
    void Update()
    {
        if(MouseLook.ml.changing == false)
            if (PlayerMovement.pm.currentPlanet != null)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.Cross(PlayerMovement.pm.gravityDirection, transform.right), -PlayerMovement.pm.gravityDirection);

                Debug.DrawRay(transform.position, Vector3.Cross(PlayerMovement.pm.gravityDirection, transform.right) * 10f, Color.green, 20f);
            }  
            else
            {
                transform.rotation = Quaternion.LookRotation(MouseLook.ml.pCamera.forward + (MouseLook.ml.mouseY * MouseLook.ml.pCamera.up * 0.01f) + (MouseLook.ml.mouseX * MouseLook.ml.pCamera.right * 0.01f), MouseLook.ml.pCamera.up + (MouseLook.ml.mouseY * -MouseLook.ml.pCamera.forward * 0.01f));
            }
                
    }
}
