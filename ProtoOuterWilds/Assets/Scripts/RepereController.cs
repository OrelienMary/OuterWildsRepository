using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepereController : MonoBehaviour
{
    public Material unactiveMaterial;
    public Material activeMaterial;

    public MeshRenderer forwardMesh;
    public MeshRenderer behindMesh;
    public MeshRenderer upMesh;
    public MeshRenderer downMesh;
    public MeshRenderer rightMesh;
    public MeshRenderer leftMesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.pm.currentPlanet != null)
            transform.rotation = Quaternion.LookRotation(Vector3.Cross(PlayerMovement.pm.gravityDirection, MouseLook.ml.pCamera.right), -PlayerMovement.pm.gravityDirection);
        else
            transform.localRotation = Quaternion.Euler(Vector3.zero);

        if (PlayerMovement.pm.z < 0)
        {
            ActiveRepere(forwardMesh);
        }
        else
        {
            UnactiveRepere(forwardMesh);
        }

        if (PlayerMovement.pm.z > 0)
        {
            ActiveRepere(behindMesh);
        }
        else
        {
            UnactiveRepere(behindMesh);
        }

        if (PlayerMovement.pm.x < 0)
        {
            ActiveRepere(rightMesh);
        }
        else
        {
            UnactiveRepere(rightMesh);
        }

        if (PlayerMovement.pm.x > 0)
        {
            ActiveRepere(leftMesh);
        }
        else
        {
            UnactiveRepere(leftMesh);
        }

        if (PlayerMovement.pm.down > 0)
        {
            ActiveRepere(upMesh);
        }
        else
        {
            UnactiveRepere(upMesh);
        }

        if (PlayerMovement.pm.up > 0)
        {
            ActiveRepere(downMesh);
        }
        else
        {
            UnactiveRepere(downMesh);
        }
    }

    void ActiveRepere(MeshRenderer mesh)
    {
        mesh.material = activeMaterial;
        mesh.transform.parent.localScale = new Vector3(1, 1.75f, 1);
    }

    void UnactiveRepere(MeshRenderer mesh)
    {
        mesh.material = unactiveMaterial;
        mesh.transform.parent.localScale = new Vector3(1, 1, 1);
    }
}
