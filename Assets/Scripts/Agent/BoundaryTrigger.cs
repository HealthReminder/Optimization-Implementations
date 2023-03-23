using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryTrigger : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<OnReturnToPool>())
        {
            Debug.Log("pol");

            other.GetComponent<OnReturnToPool>().Return() ;
            Debug.Log("Returned object to pool");
        }
        if (other.transform.parent != null)
            if (other.transform.parent.GetComponent<OnReturnToPool>())
                other.transform.parent.GetComponent<OnReturnToPool>().Return();
    }
}
