using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class FireExtinguisher : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float charge = 1f;
    [SerializeField] private SteamVR_Action_Boolean pressAction;
    [SerializeField] private FireExtinguisherType extinguisherType;
    [SerializeField] private GameObject extrudedParticles;
    [SerializeField] private PressureGauge pressureGauge;  

    private void StartExtruding()
    {
       extrudedParticles.SetActive(true);
    }

    private void StopExtruding()
    {
        extrudedParticles.SetActive(false);
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
