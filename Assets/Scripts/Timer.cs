using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float seconds { get; private set; }

    private void Awake()
    {
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
