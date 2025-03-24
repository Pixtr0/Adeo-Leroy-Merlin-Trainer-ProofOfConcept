using System;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    public EventHandler<float> OnLevelCompleted;
    public float timer = 0;
    public float completionDelay;
    public bool completed;
    bool runTimer;

    private void Start()
    {
        timer = 0;
        runTimer = true;
    }
    private void Update()
    {
        if(runTimer)
            timer += Time.deltaTime;
        else
        {
            completionDelay -= Time.deltaTime;
            if(completionDelay <= 0)
            {
                OnLevelCompleted.Invoke(this, timer);
                completed = true;
            }
        }
    }

    public void CompleteLevel()
    {
        runTimer = false;
    }
}
