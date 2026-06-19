using UnityEngine;
using Colosso.Tools.Audio;

public class PlayAudioOnPosition : MonoBehaviour
{
    [SerializeField] private SFXSets _sfxSet;
    [SerializeField] private float _volume;
    [SerializeField] private float _pitchDefault = 1;
    [SerializeField] private float _pitchRange = 0;
    public void PlaySound(Vector3 pos)
    {
        AudioManager.Instance?.PlaySound(_sfxSet, _volume, transform.position, _pitchDefault, _pitchRange);
    }
}
