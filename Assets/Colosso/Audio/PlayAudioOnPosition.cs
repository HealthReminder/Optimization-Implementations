using UnityEngine;

namespace Colosso.Tools.Audio
{
    public class PlayAudioOnPosition : MonoBehaviour
    {
        [SerializeField] private SFXSets _sfxSet = SFXSets.None;
        [SerializeField] private float volume;
        [SerializeField] private float pitchDefault = 1;
        [SerializeField] private float pitchRange = 0;

        private void Start()
        {
            if (_sfxSet == SFXSets.None)
                Debug.LogError(gameObject.name + " don't have a sfx set");
        }
        public void PlaySound(Vector3 pos)
        {
            AudioManager.Instance.PlaySound(_sfxSet, volume, pos, pitchDefault, pitchRange);
        }
    }
}