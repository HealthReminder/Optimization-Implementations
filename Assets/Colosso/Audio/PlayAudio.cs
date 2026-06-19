using UnityEngine;

namespace Colosso.Tools.Audio
{
    public class PlayAudio : MonoBehaviour
    {
        [SerializeField] private SFXSets _sfxSet = SFXSets.None;
        [Range(0f, 5f)]
        [SerializeField] private float volume;
        [Range(0.1f, 2f)]
        [SerializeField] private float pitchDefault = 1;
        [Range(0f, 1f)]
        [SerializeField] private float pitchRange = 0;
        private void Start()
        {
            if (_sfxSet == SFXSets.None)
                Debug.LogError(gameObject.name + " don't have a sfx set");
        }
        public void PlaySound()
        {
            if (!AudioManager.Instance) return;
            AudioManager.Instance.PlaySound(_sfxSet, volume, transform.position, pitchDefault, pitchRange);
        }
    }
}