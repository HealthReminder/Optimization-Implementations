using System.Collections;
using UnityEngine;

public class Level1 : Level
{
    public Lamp LampAlarm;
    public override void OnStart()
    {
        base.OnStart();
        StartCoroutine(StartRoutine());
    }
    IEnumerator StartRoutine()
    {
        //yield return new WaitForSeconds(2);
        LampAlarm.TurnOn();
        CircleSpawner.Spawn();
        //yield return new WaitForSeconds(5);
        //LampAlarm.TurnOff();
        //yield return new WaitForSeconds(2);
        //LampAlarm.TurnOn();
        yield return new WaitForSeconds(2);
        BoxSpawner.SpawnBoxes(50);
        yield return new WaitForSeconds(5);
        LampAlarm.TurnOff();



        yield break;
    }
    public override void OnComplete()
    {
        base.OnComplete();
    }
    public override void OnFail()
    {
        base.OnFail();
    }
}
