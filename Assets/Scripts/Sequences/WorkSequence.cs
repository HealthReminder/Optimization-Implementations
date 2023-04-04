using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkSequence : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private BoxCountTrigger _boxCounter;

    public IEnumerator StartRoutine()
    {


        for (int i = 0; i < 3; i++)
        {
            // Call out current level
            StartCoroutine(_levelManager.TVMessage((3 - i).ToString(), true));
            _levelManager.TVScreen.IsBlinking = true;
            yield return new WaitForSeconds(3);
            _levelManager.TVScreen.IsBlinking = false;
            _levelManager.TVScreen.TurnOff();
            yield return new WaitForSeconds(1);

            // Move boxes to conveyor
            yield return StartCoroutine(_levelManager.TVMessage("WATCH OUT FOR FALLING BOXES", true));
            _levelManager.TVScreen.IsBlinking = true;
            yield return new WaitForSeconds(2);
            _levelManager.lampAlarm.TurnOn();
            yield return new WaitForSeconds(2);
            _levelManager._boxSpawner.SpawnBoxes(5 + i * i * i * 5);
            yield return new WaitForSeconds(2);
            _levelManager.lampAlarm.TurnOff();
            yield return new WaitForSeconds(1);
            _levelManager.TVScreen.IsBlinking = true;
            StartCoroutine(_levelManager.TVMessage("DELIVER THE BOXES", true));

            while (_boxCounter.CurrentCount < 4 + i * i * i * 5) { 

                Debug.Log(_boxCounter.CurrentCount);
                yield return null;
            }

            _levelManager.TVScreen.IsBlinking = false;
            StartCoroutine(_levelManager.TVMessage("GOOD    ENOUGH", true));
            yield return new WaitForSeconds(2);


        }

        StartCoroutine(_levelManager.TVMessage("YOU    WIN      CONGRATS", true));

        yield return new WaitForSeconds(3);

        yield break;
    }


}
