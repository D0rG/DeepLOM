using UnityEngine;

[AddComponentMenu("_DeepLOM/UI_menu")]
public class UI_menu : MonoBehaviour
{
    [SerializeField] private RadioButtonSet RadioButtonSet;

    private void Awake()
    {

    }

    public void PressButton(int scriptIndex)
    {
        EmulationScript currScript = (EmulationScript)scriptIndex;
    }
}
public enum EmulationType
{
    [InspectorName("Обучение")]
    Studing,
    [InspectorName("Экзамен")]
    Exam
}

public enum EmulationScript
{
    [InspectorName("Пожар")]
    Fire,
    [InspectorName("Проблема с проводкой")]
    Wire
}
