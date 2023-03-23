using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private string setName;
    [SerializeField] private float volume;
    [SerializeField] private float pitchDefault = 1;
    [SerializeField] private float pitchRange = 0;
    public void PlaySound()
    {
        AudioManager.Instance.SpawnSound(setName, volume, transform.position, pitchDefault, pitchRange);
    }
}
