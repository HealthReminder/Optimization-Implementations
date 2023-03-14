using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float _timeElapsed = 0.0f;
    float _nextTrigger = 8f;
    public CircleSpawner OutsideBoxSpawner;
    private void Update()
    {
        _timeElapsed += Time.deltaTime;
        if(_timeElapsed >= _nextTrigger)
        {
            Debug.Log("Exterior box spawn activated");
            OutsideBoxSpawner.InstantiateTimed();
            _nextTrigger += 8;
        }
    }
}
