using UnityEngine;
using UnityEngine.Events;

public class CustomEvents : MonoBehaviour
{
    [System.Serializable]
    public class TransformEvent : UnityEvent<Transform>
    {

    }
    [System.Serializable]
    public class Vector3Event : UnityEvent<Vector3>
    {

    }
}
