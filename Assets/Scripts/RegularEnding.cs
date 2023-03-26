using System.Collections;
using UnityEngine;

public class RegularEnding : MonoBehaviour
{
    [SerializeField] private CircleSpawner _circleSpawner;
    [SerializeField] private BoxSpawner _boxSpawner;
    [SerializeField] private Lamp _lampAlarm;
    [SerializeField] private TVScreen _TVScreen;
    [SerializeField] private ConveyorBelt _ConveyorBelt;
    [SerializeField] private Lamp[] TitleLights;
    [SerializeField] private Lamp[] BeltLights;
    [SerializeField] private Lamp CenterLight;
    [SerializeField] private Lamp LeverLight;
    public void Initiate()
    {
        StartCoroutine(StartRoutine());
    }
    IEnumerator StartRoutine()
    {
        foreach (Lamp lamp in TitleLights)
        {
            lamp.TurnOn();
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(2);
        CenterLight.TurnOn();
        yield return new WaitForSeconds(1);
        yield return ShowMessageRoutine("WELCOME TO THE FAMILY");
        yield return ShowMessageRoutine("YOUR SHIFT STARTS NOW", true);
        yield return new WaitForSeconds(0.5f);
        foreach (Lamp lamp in BeltLights)
        {
            lamp.TurnOn();
            yield return new WaitForSeconds(0.5f);
        }
        yield return ShowMessageRoutine("TURN THE CONVEYOR BELT ON", true);
        yield return new WaitForSeconds(1);
        LeverLight.TurnOn();
        while (!_ConveyorBelt.IsOn)
            yield return BlinkTVScreen();
        yield return new WaitForSeconds(2);
        yield return ShowMessageRoutine("GOOD JOB");


        _lampAlarm.TurnOn();
        yield return new WaitForSeconds(1);

        _circleSpawner.Spawn();
        yield return ShowMessageRoutine("WATCH OUT FOR FALLING BOXES");

        _boxSpawner.SpawnBoxes(3);
        yield return new WaitForSeconds(5);
        _TVScreen.TurnOff();

        yield return new WaitForSeconds(1);
        _lampAlarm.TurnOff();

        _TVScreen.ChangeText("PUT ALL THE BOXES ON THE BELT");



        yield break;
    }
    IEnumerator BlinkTVScreen()
    {
        yield return new WaitForSeconds(0.5f);
        _TVScreen.TurnOn();
        yield return new WaitForSeconds(1);
        _TVScreen.TurnOff();
    }
    IEnumerator ShowMessageRoutine(string txt, bool leaveOn = false)
    {

        _TVScreen.TurnOn();
        _TVScreen.ChangeText(txt);
        yield return new WaitForSeconds(2.5f);

        if(!leaveOn)
            _TVScreen.TurnOff();
        yield return new WaitForSeconds(1);

        yield break;
    }
}
