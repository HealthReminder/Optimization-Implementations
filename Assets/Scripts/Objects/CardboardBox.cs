using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardBox : MonoBehaviour
{
    public GameObject[] PossibleBoxes;
    private int _currentBox;
    private void OnEnable()
    {
        if (PossibleBoxes == null)
            Debug.LogError("Null possible boxes On Enable.");
        if (PossibleBoxes.Length < 1)
            Debug.LogError("Not enough possible boxes On Enable.");

        int newId = Random.Range(0, PossibleBoxes.Length);
        while (newId == _currentBox)
            newId = Random.Range(0, PossibleBoxes.Length);
        PossibleBoxes[_currentBox].SetActive(false);
        PossibleBoxes[newId].SetActive(true);
        _currentBox = newId; 


    }
}
