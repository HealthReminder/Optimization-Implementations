using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField]private GameObject _objOn;
    [SerializeField]private GameObject _objOff;
    private bool _isOn = false;
    private void Start()
    {
        UpdateState();
    }
    [ContextMenu("Toggle")]
    public void Toggle()
    {
        _isOn = !_isOn;
        UpdateState();
    }
    [ContextMenu("Turn ON")]
    public void TurnOn()
    {
        _isOn = true;
        UpdateState();
    }
    [ContextMenu("Turn OFF")]
    public void TurnOff()
    {
        _isOn = false;
        UpdateState();
    }
    private void UpdateState()
    {
        if(_isOn)
        {
            _objOn.SetActive(true);
            _objOff.SetActive(false);
        } else
        {
            _objOff.SetActive(true);
            _objOn.SetActive(false);
        }
    }
}
