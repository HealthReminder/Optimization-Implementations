using System.Collections;
using UnityEngine;

public class RegularEnding : MonoBehaviour
{
    [SerializeField] private CircleSpawner _circleSpawner;
    [SerializeField] private BoxSpawner _boxSpawner;
    [SerializeField] private Lamp _lampAlarm;
    [SerializeField] private TVScreen _TVScreen;
    public void Initiate()
    {
        StartCoroutine(StartRoutine());
    }
    IEnumerator StartRoutine()
    {
        _TVScreen.TurnOn();
        _TVScreen.ChangeText("YOUR NEW JOB STARTS NOW");
        _circleSpawner.Spawn();
        yield return new WaitForSeconds(3);

        _TVScreen.TurnOff();
        yield return new WaitForSeconds(1);

        _lampAlarm.TurnOn();
        yield return new WaitForSeconds(1);

        _TVScreen.TurnOn();
        _TVScreen.ChangeText("WATCH OUT FOR FALLING BOXES");
        _circleSpawner.Spawn();

        yield return new WaitForSeconds(1);

        _boxSpawner.SpawnBoxes(50);
        yield return new WaitForSeconds(5);
        _TVScreen.TurnOff();

        yield return new WaitForSeconds(1);
        _lampAlarm.TurnOff();

        _TVScreen.ChangeText("PUT ALL THE BOXES ON THE BELT");
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _TVScreen.TurnOn();
            yield return new WaitForSeconds(1);
            _TVScreen.TurnOff();
            _circleSpawner.Spawn();

        }



        yield break;
    }
}
