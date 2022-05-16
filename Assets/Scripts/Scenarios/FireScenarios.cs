using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("_DeepLOM/FireScenarios")]
public class FireScenarios : Scenarios
{
    [SerializeField] private FirePoint[] firePoints;
    [SerializeField] private TMPro.TextMeshProUGUI helpText;
    [Multiline]
    [SerializeField] private string[] helpInfo;
    private int helpIndex = 0;

    public override void StartScenarios(EmulationType type)
    {
        emulationType = type;
        started = true;
        firePoints[Random.Range(0, firePoints.Length - 1)].StartBurning();

        if (type == EmulationType.Exam)
        {
            helpInfo = new string[0];
            helpText.text = "Подсказок на экзамене нет.";
        }

        HelpStep();
    }

    public override void HelpStep(string appendText = "")
    {
        if (helpInfo.Length > 0 && helpInfo.Length >= helpIndex + 1)
        {
            helpText.text = helpInfo[helpIndex++];
        }

        helpText.text += "\n" + appendText;
    }
}
