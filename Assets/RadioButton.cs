using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RadioButton : MonoBehaviour
{
    public EmulationType emulationType;
    public Toggle togle;
    [HideInInspector] public EmulationTypeEvent onRadioButtnChange = new EmulationTypeEvent();  

    private void Awake()
    {
        togle.onValueChanged.AddListener((state) => 
        {
            if (state)
            {
                onRadioButtnChange.Invoke(emulationType);
            }
        });
    }
}

public class EmulationTypeEvent : UnityEvent<EmulationType>
{

}
