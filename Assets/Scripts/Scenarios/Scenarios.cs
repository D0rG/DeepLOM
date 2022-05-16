using UnityEngine;

[AddComponentMenu("_DeepLOM/Scenarios")]
public class Scenarios : MonoBehaviour
{
    protected EmulationType emulationType;
    public bool started;  

    public virtual void StartScenarios(EmulationType type)
    {
        emulationType = type;   
        started = true;
    }

    public virtual void HelpStep(string apendText = "")
    {

    }
}
