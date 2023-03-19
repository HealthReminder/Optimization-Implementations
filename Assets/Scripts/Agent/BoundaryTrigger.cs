using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryTrigger : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<ReturnToPool>())
        {
            Debug.Log("pol");

            other.GetComponent<ReturnToPool>().Return() ;
            Debug.Log("Returned object to pool");
        }
        if (other.transform.parent != null)
            if (other.transform.parent.GetComponent<ReturnToPool>())
                other.transform.parent.GetComponent<ReturnToPool>().Return();
    }
}
