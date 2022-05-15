using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class PowerSwitch : MonoBehaviour
{
    public static PowerSwitch instance;
    [SerializeField] private Interactable interactable;
    [SerializeField] private GameObject ButtonObject;
    [SerializeField] private LinearMapping linearMapping;

    [HideInInspector] public UnityEvent onPowerStatusChange;
    public bool powerEnabled {  get; private set; }
    private bool _powerEnabled
    {
        get
        {
            return powerEnabled;
        }
        set
        {
            powerEnabled = value;
            onPowerStatusChange.Invoke();
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        powerEnabled = true;
    }

    private void Update()
    {
        if ((!_powerEnabled && linearMapping.value == 0) || (_powerEnabled && linearMapping.value == 1))
        {
            Detach();
            _powerEnabled = !_powerEnabled;
        }
    }

    public void Detach()
    {
        if (interactable.attachedToHand != null)
        {
            interactable.attachedToHand.DetachObject(ButtonObject);
        }
    }
}


