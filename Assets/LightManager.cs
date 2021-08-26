using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] //exectute methods in class while not in game.
public class LightManager : MonoBehaviour
{
    [SerializeField]
    private Light dirLight;
    [SerializeField]
    private LightingCond preset;
    [SerializeField]
    private int clamp = 480;
    [SerializeField, Range(0, 480)]
    private float TOD;

    

    private void UpdateLightSettings(float timePer) 
    {
        RenderSettings.ambientLight = preset.ambiColor.Evaluate(timePer); // change color ... based on the time of day
        RenderSettings.fogColor = preset.fogColor.Evaluate(timePer);
        RenderSettings.fogDensity += preset.fogDensity;

        if (dirLight != null)
        {
            dirLight.color = preset.dirColor.Evaluate(timePer);
            dirLight.transform.localRotation = Quaternion.Euler(new Vector3((timePer * 360f) -90f, -170, 0));
        }
    }

   
    // Update is called once per frame
    void Update()
    {
        if (preset == null) 
        {
            return;
        }
        if (Application.isPlaying) 
        {
            TOD += Time.deltaTime;
            TOD %= clamp;
            UpdateLightSettings(TOD/ clamp);

        }
        else 
        {
            UpdateLightSettings(TOD / clamp);
        }


     
    }
}
