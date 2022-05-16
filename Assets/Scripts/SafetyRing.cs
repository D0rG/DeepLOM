using UnityEngine;
using Valve.VR.InteractionSystem;

[AddComponentMenu("_DeepLOM/SafetyRing")]
public class SafetyRing : MonoBehaviour
{
    [SerializeField] private LinearMapping linearMapping;
    [SerializeField] private Interactable interactable;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private LinearDrive linearDrive;
    public bool ringConnected { get; private set; }
    private bool detached = false;

    private void Awake()
    {
        ringConnected = true;
    }

    private void Update()
    {
        if (linearMapping.value >= 1f && !detached)
        {
            Detach();
        }
    }

    private void Detach()
    {
        if (interactable.attachedToHand != null)
        {
            linearDrive.enabled = false;
            interactable.attachedToHand.DetachObject(this.gameObject);
            detached = true;
            interactable.enabled = false;
            gameObject.transform.parent = null;
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;

            ringConnected = false;
        }
    }
}
