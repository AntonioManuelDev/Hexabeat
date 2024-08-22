using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    [SerializeField] private float bpm;
    [SerializeField] private AudioSource aSource;
    [SerializeField] private Intervals[] intervals;

    private void Update()
    {
        foreach(Intervals interval in intervals)
        {
            float sampledTime = (aSource.timeSamples /(aSource.clip.frequency * interval.GetIntervalLength(bpm)));
            interval.CheckForNewInterval(sampledTime);
        }
    }

    public void AddMe(GameObject self)
    {
        // Ensure the object has a PulseToTheBeat component
        PulseToTheBeat pulseToTheBeat = self.GetComponent<PulseToTheBeat>();

        // Create a UnityAction that points to the Pulse method
        UnityAction trigger = new UnityAction(pulseToTheBeat.Pulse);

        // Add the trigger to the desired interval
        if (intervals != null && intervals.Length > 1)
        {
            intervals[1].addTrigger(trigger);
        }
        else
        {
            Debug.LogError("Intervals array is not set or is too short.");
        }
    }
}
[System.Serializable]
public class Intervals
{
    [SerializeField] private float steps;
    [SerializeField] private UnityEvent trigger = new UnityEvent();
    private int lastInterval;
    public float GetIntervalLength(float bpm)
    {
        return 60f / (bpm * steps);
    }
    public void addTrigger(UnityAction action)
    {
        trigger.AddListener(action);
    }
    public void CheckForNewInterval(float interval)
    {
        if(Mathf.FloorToInt(interval) != lastInterval)
        {
            lastInterval = Mathf.FloorToInt(interval);
            trigger.Invoke();
        }
    }
}
