using System.Collections;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Every level script will inherit from this class with some common behaviour/variables
/// </summary>
public abstract class Level : MonoBehaviour
{
    [Header("References")]
    public BoxSpawner BoxSpawner;
    public CircleSpawner CircleSpawner;
    /*
    [Header("Events")]
    public UnityEvent[] OnStartEvents;
    public UnityEvent[] OnFailEvents;
    public UnityEvent[] OnCompleteEvents;*/
    public virtual void OnStart()
    {
        //foreach(UnityEvent e in OnStartEvents)
        //    e.Invoke();
    }
    public virtual void OnFail()
    {
        //foreach (UnityEvent e in OnFailEvents)
        //    e.Invoke();
    }
    public virtual void OnComplete()
    {
        //foreach (UnityEvent e in OnCompleteEvents)
        //    e.Invoke();
    }
}
