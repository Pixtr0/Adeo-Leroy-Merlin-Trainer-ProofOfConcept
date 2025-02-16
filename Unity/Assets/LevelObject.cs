using System;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    public EventHandler OnLevelCompleted;

    public void CompleteLevel()
    {
        OnLevelCompleted.Invoke(this,EventArgs.Empty);
    }
}
