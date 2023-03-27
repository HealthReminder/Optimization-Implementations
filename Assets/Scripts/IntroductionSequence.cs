using System.Collections;
using UnityEngine;

public class IntroductionSequence : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private GameObject[] _disableOnSkip;
    public void Skip()
    {
        StartCoroutine(SkipRoutine());
    }
    IEnumerator SkipRoutine()
    {
        foreach (GameObject g in _disableOnSkip)
            g.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        StartCoroutine(_levelManager.ToggleTitleLights(true));
        yield return new WaitForSeconds(0.1f);
        _levelManager.recordPlayer.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _levelManager.centerLight.TurnOn();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(_levelManager.ToggleBeltLights(true));
        yield return new WaitForSeconds(0.1f);
        _levelManager.leverLight.TurnOn();
        yield return new WaitForSeconds(0.25f);
        _levelManager.playerController.CanLook = _levelManager.playerController.CanMove = true;

    }
    public void Initiate()
    {
        StartCoroutine(StartRoutine());
    }
    IEnumerator StartRoutine()
    {
        // Title
        yield return _levelManager.PlayerLookAt(_levelManager.titleTransform, 0.5f);
        yield return _levelManager.ToggleTitleLights(true);
        yield return new WaitForSeconds(1);
        _levelManager.recordPlayer.SetActive(true);
        yield return new WaitForSeconds(1);
        _levelManager.centerLight.TurnOn();
        yield return new WaitForSeconds(1);

        // Welcome message
        yield return _levelManager.PlayerLookAt(_levelManager.lampAlarm.transform, 0.5f);
        yield return new WaitForSeconds(1);
        StartCoroutine(_levelManager.PlayerLookAt(_levelManager.TVScreen.transform, 0.5f));
        yield return _levelManager.ShowMessageRoutine("WELCOME TO THE FAMILY");

        // First task
        yield return _levelManager.ShowMessageRoutine("YOUR SHIFT STARTS NOW", true);
        yield return new WaitForSeconds(0.5f);
        yield return _levelManager.PlayerLookAt(_levelManager.lampAlarm.transform, 0.5f);
        yield return _levelManager.ToggleBeltLights(true);
        yield return _levelManager.PlayerLookAt(_levelManager.TVScreen.transform, 0.5f);
        StartCoroutine(_levelManager.ShowMessageRoutine("TURN THE CONVEYOR BELT ON", true));
        yield return new WaitForSeconds(1);
        yield return _levelManager.PlayerLookAt(_levelManager.lampAlarm.transform, 0.5f);
        yield return new WaitForSeconds(1);
        _levelManager.leverLight.TurnOn();
        yield return new WaitForSeconds(0.5f);
        _levelManager.playerController.CanLook = _levelManager.playerController.CanMove = true;


        while (!_levelManager.conveyorBelt.IsOn)
            yield return _levelManager.BlinkTVScreen();
        yield return new WaitForSeconds(2);
        yield return _levelManager.ShowMessageRoutine("GOOD    JOB");


        //_lampAlarm.TurnOn();
        //yield return new WaitForSeconds(1);

        //_circleSpawner.Spawn();
        //yield return ShowMessageRoutine("WATCH OUT FOR FALLING BOXES");

        //_boxSpawner.SpawnBoxes(3);
        //yield return new WaitForSeconds(5);
        //_TVScreen.TurnOff();

        //yield return new WaitForSeconds(1);
        //_lampAlarm.TurnOff();

        //_TVScreen.ChangeText("PUT ALL THE BOXES ON THE BELT");


        yield break;
    }
  
}
