using System.Collections;
using UnityEngine;

[AddComponentMenu("_DeepLOM/Timer")]
public class Timer : MonoBehaviour
{
    public static Timer instance { get; private set; }
    public float seconds { get; private set; }

    private void Awake()
    {
        instance = this;
        ClearTimer();
    }

    public void StartTimer()
    {
        StartCoroutine(TimerCorutine());
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    public void ClearTimer()
    {
        seconds = 0;
    }

    private IEnumerator TimerCorutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            seconds++;
        }
    }
}
