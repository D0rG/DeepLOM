using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

[AddComponentMenu("_DeepLOM/FireExtinguisher")]
public class FireExtinguisher : MonoBehaviour
{
    [SerializeField][Range(0, 1f)] private float charge = 1f;
    [SerializeField] private SteamVR_Action_Boolean pressAction;
    [SerializeField] private FireExtinguisherType extinguisherType;
    [SerializeField] private SafetyRing safetyRing;
    [SerializeField] private Interactable interactable;
    [SerializeField] private ParticleSystem extrudedParticles;
    [SerializeField] private PressureGauge pressureGauge;
    [SerializeField][Range(0, 10f)] private float extrusionSpeed;
    [SerializeField][Range(0, 5f)] private float extrudingLenght;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material[] materials;
    private Transform transformExtruding;

    [Header("Debug")]
    [SerializeField] private bool extrude = false; 

    private void Awake()
    {
        Extruding(false);
        transformExtruding = extrudedParticles.transform;
        meshRenderer.material = materials[(int)extinguisherType];
    }

    private void Extruding(bool isExtruding)
    {
        if (isExtruding)
        {
            extrudedParticles.Play();
            StartCoroutine(ExtrudingOnFireChech());
        }
        else
        {
            extrudedParticles.Stop();
            StopAllCoroutines();
        }
    }

    private void Update()
    {
        if (safetyRing.ringConnected) { return; }   //Кольцо предохранитель на месте

        if (interactable.attachedToHand != null) //Если взят в руку, или дебаг
        {
            if (pressAction[interactable.attachedToHand.handType].stateDown)
            {
                extrude = true;
            }
            else if (pressAction[interactable.attachedToHand.handType].stateUp)
            {
                extrude = false;
            }

            if (extrude && charge > 0)
            {
                float delta = extrusionSpeed * Time.deltaTime;
                if (charge - delta >= 0)
                {
                    charge -= delta;
                }
                else
                {
                    charge = 0;
                }
            }

            Extruding(extrude);
            pressureGauge.pressure = charge;
        }
        else
        {
            Extruding(false);
            extrude = false;
        }
    }

    private IEnumerator ExtrudingOnFireChech()
    {
        while (true)
        {
            RaycastHit hit;
            Ray ray = new Ray(transformExtruding.position, transformExtruding.right);

            if (Physics.Raycast(ray, out hit, extrudingLenght))
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.cyan);
                var fire = hit.collider.GetComponent<FirePoint>();
                if (fire != null)
                {
                    fire.PutOutFire(extinguisherType);
                }
            }

            yield  return new WaitForFixedUpdate();
        }

        yield return null;
    }
}

public enum FireExtinguisherType
{
    [InspectorName("Воздушно-пенный")]
    AirFoam,
    [InspectorName("Углекислотный")]
    CarbonDioxide, 
    [InspectorName("Порошковый")]
    Powder 
}
