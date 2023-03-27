using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The game manager observes the state of the game
/// And active and interact with objects and endings
/// </summary>
public class GameManager : MonoBehaviour
{
    public bool IsSkipIntro = false;
    [SerializeField] private CircleSpawner _circleSpawner;
    [SerializeField] private TVScreen _TVScreen;
    [SerializeField] IntroductionSequence Introduction;
    [SerializeField] WorkSequence Work;
    private void Start()
    {
        StartCoroutine(GameLoop());
    }
    IEnumerator GameLoop ()
    {
        yield return new WaitForSeconds(1);

        //_circleSpawner.Spawn();
        //yield return new WaitForSeconds(3);

        //_TVScreen.ChangeText("WELCOME TO THE FAMILY");
        //_TVScreen.TurnOn();
        //yield return new WaitForSeconds(3);

        //_TVScreen.TurnOff();
        if (IsSkipIntro)
            yield return Introduction.SkipRoutine();
        else
            yield return Introduction.StartRoutine();

        yield return new WaitForSeconds(1);

        StartCoroutine(Work.StartRoutine());

        while (true)
        {
            yield return null;
        }
        yield break;
    }
}
