using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The game manager observes the state of the game
/// And active and interact with objects and endings
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private CircleSpawner _circleSpawner;
    [SerializeField] private TVScreen _TVScreen;
    [SerializeField] RegularEnding RegularEnding;
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
        yield return new WaitForSeconds(1);
        RegularEnding.Initiate();
        while (true)
        {
            yield return null;
        }
        yield break;
    }
}
