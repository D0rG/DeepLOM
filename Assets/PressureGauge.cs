using UnityEngine;

public class PressureGauge : MonoBehaviour
{
    /// <summary>
    /// 0 - No Pressure
    /// 1 - Full Pressure
    /// </summary>
    [Range(0f, 1f)] public float pressure = 1f;
    [SerializeField] private Transform pressureArrow;
    [SerializeField] private float maxPressure = 300f;
    [SerializeField] private float minPressure = 30f;
    private float pressureStep;

    private void Awake()
    {
        pressureStep = (maxPressure - minPressure) / 100f;
    }

    private void DrawPressure()
    {
        Vector3 arrowAngle = pressureArrow.rotation.eulerAngles;
        arrowAngle.z = minPressure + (pressureStep * 100 * pressure);
        pressureArrow.rotation = Quaternion.Euler(arrowAngle);
    }

    private void FixedUpdate()
    {
        DrawPressure();
    }
}
