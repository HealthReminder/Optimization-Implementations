using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TransformEvent : UnityEvent<Transform>
{
    
}
public class ReturnToPool : MonoBehaviour
{
    [SerializeField] private TransformEvent _onReturn;
    public void Return()
    {
        _onReturn.Invoke(transform);
    }
}
