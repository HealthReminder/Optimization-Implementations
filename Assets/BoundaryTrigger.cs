using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryTrigger : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<ReturnToPool>())
        {
            other.GetComponent<ReturnToPool>().Return() ;
        }
    }
}
