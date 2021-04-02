using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityIndicatorUpdater : MonoBehaviour
{
    public TMPro.TextMeshProUGUI tmpro;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.pm.currentPlanet != null)
            tmpro.text = "x" + PlayerMovement.pm.gravityMultiplier.ToString();
        else
            tmpro.text = "x0";
    }
}
