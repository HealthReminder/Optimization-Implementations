using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkSequence : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private BoxCountTrigger _boxCounter;

    public IEnumerator StartRoutine()
    {
        StartCoroutine(_levelManager.TVMessage("WATCH OUT FOR FALLING BOXES", true));
        _levelManager.TVScreen.IsBlinking = true;
        ///Throw boxes
        yield return new WaitForSeconds(3);
        _levelManager.lampAlarm.TurnOn();
        yield return new WaitForSeconds(3);
        _levelManager._boxSpawner.SpawnBoxes(10);
        yield return new WaitForSeconds(3);
        _levelManager.lampAlarm.TurnOff();
        yield return new WaitForSeconds(1);
        _levelManager.TVScreen.IsBlinking = true;
        StartCoroutine(_levelManager.TVMessage("DELIVER THE BOXES", true));

        while (_boxCounter.CurrentCount < 5)
            yield return null;

        _levelManager.TVScreen.IsBlinking = false;
        StartCoroutine(_levelManager.TVMessage("GOOD    ENOUGH", true));



        yield return new WaitForSeconds(6);


        yield return new WaitForSeconds(3);
        yield break;
    }


}
