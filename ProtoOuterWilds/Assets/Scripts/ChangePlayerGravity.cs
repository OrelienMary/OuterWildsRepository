using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerGravity : MonoBehaviour
{
    public float planetGravity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody.tag == "Player")
        {
            PlayerMovement.pm.currentPlanet = transform;

            PlayerMovement.pm.gravityMultiplier = planetGravity;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody.tag == "Player")
        {
            PlayerMovement.pm.currentPlanet = null;

            PlayerMovement.pm.gravityMultiplier = 0f;
        }
    }
}
