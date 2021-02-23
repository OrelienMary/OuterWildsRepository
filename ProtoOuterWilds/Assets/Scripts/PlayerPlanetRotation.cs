using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanetRotation : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.Cross(PlayerMovement.pm.gravityDirection, transform.right), -PlayerMovement.pm.gravityDirection);
    }
}
