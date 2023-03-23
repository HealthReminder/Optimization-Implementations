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
        TVScreen.TurnOff();

        yield return new WaitForSeconds(1);

        CircleSpawner.Spawn();
        yield return new WaitForSeconds(3);

        TVScreen.ChangeText("WELCOME TO THE FAMILY");
        TVScreen.TurnOn();
        yield return new WaitForSeconds(3);

        TVScreen.TurnOff();
        yield return new WaitForSeconds(1);

        TVScreen.TurnOn();
        TVScreen.ChangeText("YOUR NEW JOB STARTS NOW");
        CircleSpawner.Spawn();
        yield return new WaitForSeconds(3);

        TVScreen.TurnOff();
        yield return new WaitForSeconds(1);

        LampAlarm.TurnOn();
        yield return new WaitForSeconds(1);

        TVScreen.TurnOn();
        TVScreen.ChangeText("WATCH OUT FOR FALLING BOXES");
        CircleSpawner.Spawn();

        yield return new WaitForSeconds(1);

        BoxSpawner.SpawnBoxes(50);
        yield return new WaitForSeconds(5);
        TVScreen.TurnOff();

        yield return new WaitForSeconds(1);
        LampAlarm.TurnOff();

        TVScreen.ChangeText("PUT ALL THE BOXES ON THE BELT");
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            TVScreen.TurnOn();
            yield return new WaitForSeconds(1);
            TVScreen.TurnOff();
            CircleSpawner.Spawn();

        }



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
