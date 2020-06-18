using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SetLightIntensity : MonoBehaviour
{
    Light2D light2d;
    // Start is called before the first frame update
    void Start()
    {
        light2d = GetComponent<Light2D>();
        light2d.intensity = ChangeGameBrightness.intensityValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
