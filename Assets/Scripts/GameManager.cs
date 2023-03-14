using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float _timeElapsed = 0.0f;
    float _nextShower = 5f;
    float _nextDelivery = 8f;
    public CircleSpawner OutsideBoxSpawner;
    public BoxSpawner InsideBoxSpawner;
    private void Start()
    {
    }
    private void Update()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= _nextShower)
        {
            Debug.Log("Exterior box spawn activated");
            OutsideBoxSpawner.InstantiateTimed();
            _nextShower += 8;
        }
        if (_timeElapsed >= _nextDelivery)
        {
            Debug.Log("Interior box spawn activated");
            InsideBoxSpawner.SpawnBoxes(10);
            _nextDelivery += 5;
        }
    }
}
