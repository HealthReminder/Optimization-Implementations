using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Incinerator : MonoBehaviour
{
    //public UnityEvent[] OnImpostorBox;
    //public UnityEvent[] OnCardboardBox;
    private void OnTriggerEnter(Collider other)
    {
        //if(other.GetComponent<CardboardBox>())
        ///Return object to pool if applies
        if (other.GetComponent<OnReturnToPool>())
        {
            other.GetComponent<OnReturnToPool>().Return();
        }
        if (other.transform.parent != null)
            if (other.transform.parent.GetComponent<OnReturnToPool>())
                other.transform.parent.GetComponent<OnReturnToPool>().Return();
    }
}
