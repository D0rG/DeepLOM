using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("_DeepLOM/ServerRack")]
public class ServerRack : MonoBehaviour
{
    [SerializeField] [Range(0f, 1)] private float timeStapGreen = 0.08f;
    [SerializeField] [Range(0f, 1)] private float timeStapOrange = 0.32f;
    [SerializeField] [Range(1f, 5f)] private float startRange = 3;

    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material orangeMaterial;
    [SerializeField] private Material redMaterial;

    [SerializeField] private Light[] greenLights;
    [SerializeField] private Light[] orangeLights;
    [SerializeField] private Light[] redLights;

    private readonly byte noBlinkCount = 4;
    private List<Coroutine> corutines = new List<Coroutine>(2);

    private void Awake()
    {
        InitLightsColor();
    }

    private void Start()
    {
        PowerSwitch.instance.onPowerStatusChange.AddListener((power) =>
        {
            if (power)
            {
                PowerOn();
            }
            else
            {
                PowerOff();
            }
        });

        StartBlinking();
    }

    private void StartOrangeBlinking()
    {
        var corutine = StartCoroutine(BlickCorotune(orangeLights, timeStapOrange, false));
        corutines.Add(corutine);
    }

    private void StartBlinking()
    {
        if (greenLights.Length != 0)
        {
            var corutine = StartCoroutine(BlickCorotune(greenLights, timeStapGreen));
            corutines.Add(corutine);
        }

        if (orangeLights.Length != 0)
        {
            Invoke("StartOrangeBlinking", Random.RandomRange(0f, startRange));
        }
    }

    private void PowerOff()
    {
        StopAllCoroutines();

        foreach (var greenLight in greenLights)
        {
            greenLight.enabled = false;
        }

        foreach (var orangeLight in orangeLights)
        {
            orangeLight.enabled = false;
        }

        foreach (var redLight in redLights)
        {
            redLight.enabled = false;
        }

        corutines.Clear();
    }

    private void PowerOn()
    {
        StartBlinking();

        foreach(var redLight in redLights)
        {
            redLight.enabled = true;
        }
    }

    private void InitLightsColor()
    {
        foreach (var greenLight in greenLights)
        {
            greenLight.color = greenMaterial.color;
        }

        foreach (var orangeLight in orangeLights)
        {
            orangeLight.color = orangeMaterial.color;
        }

        foreach (var redLight in redLights)
        {
            redLight.color = redMaterial.color;
        }
    }

    private IEnumerator BlickCorotune(Light[] lights, float timeStap, bool isGreenLight = true)
    {
        while (true)
        {
            foreach (var light in lights)
            {
                if (isGreenLight)
                {
                    light.enabled = Random.Range(0, 2) == 1 ? true : false;
                }
                else
                {
                    light.enabled = !light.enabled;
                }
            }
            yield return new WaitForSeconds(timeStap);
        }
    }
}
