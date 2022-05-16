using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayer : MonoBehaviour
{
    public static ScriptPlayer instance {  get; private set; }
    [SerializeField] private Scenarios[] scenarious;
    private EmulationType currType;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void DoStep(bool isLast = false)
    {
        string res = string.Empty;
        if (isLast && currType == EmulationType.Exam)
        {
            Timer.instance.StopTimer();
            res += $"Ваше вермя {Timer.instance.seconds} секунд.";
        }
        
        foreach (Scenarios scenario in scenarious)
        {
            if (scenario.started)
            {
                scenario.HelpStep(res);
            }
        }
    }

    public void StartScript(EmulationType type, EmulationScript script)
    {
        currType = type;
        Debug.Log($"Start {type} with {script}");
        scenarious[(int)script].StartScenarios(type);
        Timer.instance.ClearTimer();
        Timer.instance.StartTimer();
    }
}
