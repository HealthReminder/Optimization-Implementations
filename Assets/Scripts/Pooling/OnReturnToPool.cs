using UnityEngine;
using static CustomEvents;

public class OnReturnToPool : MonoBehaviour
{
    [SerializeField] private TransformEvent _onReturn;
    public void Return()
    {
        _onReturn.Invoke(transform);
    }
}
