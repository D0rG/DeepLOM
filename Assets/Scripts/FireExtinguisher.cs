using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class FireExtinguisher : MonoBehaviour
{
    [SerializeField][Range(0, 1f)] private float charge = 1f;
    [SerializeField] private SteamVR_Action_Boolean pressAction;
    [SerializeField] private FireExtinguisherType extinguisherType;
    [SerializeField] private SafetyRing safetyRing;
    [SerializeField] private Interactable interactable;
    [SerializeField] private GameObject extrudedParticles;
    [SerializeField] private PressureGauge pressureGauge;
    [SerializeField][Range(0, 10f)] private float extrusionSpeed;

    [Header("Debug")]
    [SerializeField] private bool extrude = false;  //Для проверки огнетушителей без шлема

    private void StartExtruding()
    {
        Extruding(true);
    }

    private void StopExtruding()
    {
        Extruding(false);
    }

    private void Extruding(bool isExtruding)
    {
        extrudedParticles.SetActive(isExtruding);
        extrude = isExtruding;
    }

    private void Update()
    {
        if (safetyRing.ringConnected) { return; }   //Кольцо предохранитель на месте

        if (interactable.attachedToHand != null || extrude)
        {
            SteamVR_Input_Sources source = SteamVR_Input_Sources.Camera;

            if (!extrude) 
            {
                source = interactable.attachedToHand.handType;
            }
            

            if ((extrude || pressAction[source].stateDown) && charge > 0)
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
            pressureGauge.pressure = charge;
        }
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
