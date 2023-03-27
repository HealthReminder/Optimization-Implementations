using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkSequence : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;

    public IEnumerator StartRoutine()
    {
        StartCoroutine(_levelManager.TVMessage("WATCH OUT FOR FALLING BOXES", true));
        _levelManager.TVScreen.IsBlinking = true;
        yield return new WaitForSeconds(3);
        _levelManager.lampAlarm.TurnOn();
        yield return new WaitForSeconds(3);
        _levelManager._boxSpawner.SpawnBoxes(25);
        yield return new WaitForSeconds(3);
        _levelManager.lampAlarm.TurnOff();

        yield return new WaitForSeconds(6);

        _levelManager.TVScreen.IsBlinking = false;

        yield return new WaitForSeconds(3);
        yield break;
    }


}
