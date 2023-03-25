using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Counts how many cardboard boxes has entered the trigger
/// </summary>
public class BoxCountTrigger : MonoBehaviour
{
    [SerializeField] private int _currentCount; // How many boxes entered the trigger so far
    /// <summary>
    /// Count up as boxes enter the trigger
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        Transform boxParent = other.transform.parent;
        if (boxParent)
            if (boxParent.GetComponent<CardboardBox>())
                _currentCount++;
    }
    /// <summary>
    /// Draw a wireframe box to show the trigger in the editor
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
