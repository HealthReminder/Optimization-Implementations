using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Level Level1;
    private void Start()
    {
        Level1.OnStart();
    }
}
