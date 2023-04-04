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
    public IEnumerator SkipRoutine()
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
    public IEnumerator StartRoutine()
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
        //yield return new WaitForSeconds(0.5f);
        StartCoroutine(_levelManager.PlayerLookAt(_levelManager.TVScreen.transform, 0.5f));
        yield return _levelManager.TVMessage("GOOD    JOB");
        yield return _levelManager.TVMessage("THIS IS YOUR FINAL SHIFT");

        // First task
        yield return new WaitForSeconds(0.5f);
        yield return _levelManager.PlayerLookAt(_levelManager.lampAlarm.transform, 0.5f);
        yield return _levelManager.ToggleBeltLights(true);
        yield return _levelManager.PlayerLookAt(_levelManager.TVScreen.transform, 0.5f);
        StartCoroutine(_levelManager.TVMessage("TURN THE CONVEYOR BELT ON", true));
        yield return new WaitForSeconds(1);
        yield return _levelManager.PlayerLookAt(_levelManager.lampAlarm.transform, 0.5f);
        yield return new WaitForSeconds(1);
        _levelManager.leverLight.TurnOn();
        yield return new WaitForSeconds(0.5f);
        _levelManager.playerController.CanLook = _levelManager.playerController.CanMove = true;

        _levelManager.TVScreen.IsBlinking = true;
        while (!_levelManager.conveyorBelt.IsOn)
            yield return null;
        _levelManager.TVScreen.IsBlinking = false;

        yield return new WaitForSeconds(2);
        yield return _levelManager.TVMessage("DO NOT LET TIME RUN OUT");
        yield return _levelManager.TVMessage("YOUR SHIFT STARTS NOW", true);


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
