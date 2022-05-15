using System.Collections;
using UnityEngine;

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

    private void Awake()
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

    private void Start()
    {
        if (greenLights.Length != 0)
        {
            StartCoroutine(BlickCorotune(greenLights, timeStapGreen));
        }

        if (orangeLights.Length != 0)
        {
            Invoke("StartOrangeBlinking", Random.RandomRange(0f, startRange));
        }
    }

    private void StartOrangeBlinking()
    {
        StartCoroutine(BlickCorotune(orangeLights, timeStapOrange, false));
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
