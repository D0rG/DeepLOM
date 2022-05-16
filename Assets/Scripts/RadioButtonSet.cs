using UnityEngine;

[AddComponentMenu("_DeepLOM/RadioButtonSet")]
public class RadioButtonSet : MonoBehaviour
{
    [SerializeField] private RadioButton[] buttons;
    public EmulationType emulationType { get; private set; }

    private void Awake()
    {
        foreach(var button in buttons)
        {
            button.togle.isOn = false;
            button.onRadioButtnChange.AddListener((type) => RadioButtonChange(type));
        }

        if(buttons.Length > 0)
        {
            emulationType = buttons[0].emulationType;
            buttons[0].togle.isOn = true;
        }
    }

    private void RadioButtonChange(EmulationType emulationType)
    {
        foreach (var button in buttons)
        {
            if (button.emulationType != emulationType)
            {
                button.togle.isOn = false;
            }
        }
    }
}
