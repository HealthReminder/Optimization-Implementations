using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The Hydraulic Press is responsible for destroying boxes
/// </summary>
public class HydraulicPress : MonoBehaviour
{

    public bool IsOn { get; set; }
    [SerializeField] private BackAndForthMovement _arm;

    [SerializeField] private float _delay = 1.5f;
    float elapsedTime = 0;
    float armPos = 0;

    private void Update()
    {
        if (IsOn)
        {
            // Trigger movement every interval
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= _delay)
            {
                elapsedTime = 0;
                _arm.Move();
            }
        }
        else
        {
            // Retract arm if the press is OFF
            if (!_arm.IsRetracted && !_arm.IsMoving)
            {
                _arm.Move();
            }
        }
    }
}
