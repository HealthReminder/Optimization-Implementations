using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The Level Manager is the middleman between the action sequences
/// And the actualy world, besides it stores objects in one place only
/// </summary>
public class LevelManager : MonoBehaviour
{
    [Header("Spawners")]
    [SerializeField] internal CircleSpawner _circleSpawner;
    [SerializeField] internal BoxSpawner _boxSpawner;

    [Header("Actors")]

    [SerializeField] internal PlayerController playerController;
    [SerializeField] internal LookAtSmooth playerLookAt;
    [SerializeField] internal TVScreen TVScreen;
    [SerializeField] internal ConveyorBelt conveyorBelt;

    [Header("Props")]
    [SerializeField] internal Transform titleTransform;
    [SerializeField] internal GameObject recordPlayer;
     

    [Header("Lights")]
    [SerializeField] internal Lamp lampAlarm;
    [SerializeField] internal Lamp[] titleLights;
    [SerializeField] internal Lamp[] beltLights;
    [SerializeField] internal Lamp centerLight;
    [SerializeField] internal Lamp leverLight;
    internal IEnumerator PlayerLookAt(Transform t, float speed, bool enableLook = false)
    {
        playerController.CanLook = false;
        yield return playerLookAt.LookAtRoutine(t, speed);
        playerController.CanLook = enableLook;
        yield break;
    }
    internal IEnumerator ToggleTitleLights(bool isOn)
    {
        foreach (Lamp lamp in titleLights)
        {
            if (isOn)
                lamp.TurnOn();
            else
                lamp.TurnOff();
            yield return new WaitForSeconds(1);
        }
    }
    internal IEnumerator ToggleBeltLights(bool isOn)
    {
        foreach (Lamp lamp in beltLights)
        {
            if (isOn)
                lamp.TurnOn();
            else
                lamp.TurnOff();
            yield return new WaitForSeconds(0.5f);
        }
    }
    internal IEnumerator TVMessage(string txt, bool leaveOn = false)
    {
        TVScreen.ChangeText(txt);
        TVScreen.TurnOn();
        yield return new WaitForSeconds(2.5f);

        if (!leaveOn)
            TVScreen.TurnOff();
        yield return new WaitForSeconds(1);

        yield break;
    }
    
}
