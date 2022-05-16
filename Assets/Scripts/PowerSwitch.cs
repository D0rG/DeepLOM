using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

[AddComponentMenu("_DeepLOM/PowerSwitch")]
public class PowerSwitch : MonoBehaviour
{
    public static PowerSwitch instance;
    [SerializeField] private Interactable interactable;
    [SerializeField] private GameObject ButtonObject;
    [SerializeField] private LinearMapping linearMapping;

    [HideInInspector] public BoolUnityEvent onPowerStatusChange = new BoolUnityEvent();
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
            onPowerStatusChange.Invoke(value);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        powerEnabled = true;

        onPowerStatusChange.AddListener((status) =>
            {
                if (!status)
                {
                    ScriptPlayer.instance.DoStep();
                }
            }
        );
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

public class BoolUnityEvent : UnityEvent<bool>
{
    //
}


