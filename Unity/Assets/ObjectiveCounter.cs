using UnityEngine;
using UnityEngine.Events;

public class ObjectiveCounter : MonoBehaviour
{
    [SerializeField] int AmountOfObjectives;
    [SerializeField] int CompletedObjectives;

    public UnityEvent OnMaxObjectivesReached;

    public void CompleteObjective()
    {
        CompletedObjectives++;
        if (CompletedObjectives >= AmountOfObjectives)
        {
            OnMaxObjectivesReached.Invoke();
        }
        
    }
}
