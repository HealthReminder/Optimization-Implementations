using Colosso.Tools.Audio;

using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private SFXSets _sfxSet;
    [SerializeField] private float _volume;
    [SerializeField] private float _pitchDefault = 1;
    [SerializeField] private float _pitchRange = 0;
    public void PlaySound()
    {
        AudioManager.Instance?.PlaySound(_sfxSet, _volume, transform.position, _pitchDefault, _pitchRange);
    }
}
