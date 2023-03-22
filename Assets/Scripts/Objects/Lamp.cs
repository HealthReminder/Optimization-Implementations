using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public bool IsOn = false;
    [SerializeField]private GameObject _objOn;
    [SerializeField]private GameObject _objOff;
    private void Start()
    {
        UpdateState();
    }
    [ContextMenu("Toggle")]
    public void Toggle()
    {
        IsOn = !IsOn;
        UpdateState();
    }
    [ContextMenu("Turn ON")]
    public void TurnOn()
    {
        IsOn = true;
        UpdateState();
    }
    [ContextMenu("Turn OFF")]
    public void TurnOff()
    {
        IsOn = false;
        UpdateState();
    }
    private void UpdateState()
    {
        if(IsOn)
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
