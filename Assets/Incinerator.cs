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
        if (other.GetComponent<ReturnToPool>())
        {
            other.GetComponent<ReturnToPool>().Return();
        }
        if (other.transform.parent != null)
            if (other.transform.parent.GetComponent<ReturnToPool>())
                other.transform.parent.GetComponent<ReturnToPool>().Return();
    }
}
