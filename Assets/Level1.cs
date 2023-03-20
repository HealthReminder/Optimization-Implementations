using System.Collections;
using UnityEngine;

public class Level1 : Level
{
    public Lamp LampAlarm;
    public TVScreen TVScreen;
    public override void OnStart()
    {
        base.OnStart();
        StartCoroutine(StartRoutine());
    }
    IEnumerator StartRoutine()
    {
        yield return new WaitForSeconds(1);

        CircleSpawner.Spawn();
        TVScreen.ChangeText("WELCOME TO THE FAMILY");
        TVScreen.TurnOn();
        yield return new WaitForSeconds(2);

        TVScreen.TurnOff();
        yield return new WaitForSeconds(1);

        LampAlarm.TurnOn();
        yield return new WaitForSeconds(1);

        TVScreen.TurnOn();
        TVScreen.ChangeText("WATCH OUT falling boxes");
        
        yield return new WaitForSeconds(1);

        BoxSpawner.SpawnBoxes(50);
        yield return new WaitForSeconds(5);
        TVScreen.TurnOff();

        yield return new WaitForSeconds(1);
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
