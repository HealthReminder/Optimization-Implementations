using AYellowpaper.SerializedCollections;
using Colosso.Tools.Patterns;
using System.Collections;
using UnityEngine;

namespace Colosso.Tools.Audio
{
    /// <summary>
    /// Create a class SFXSets to use the Audio Manager
    /// Uses the SerializedDictionary from the AYellowPaper namespace. Install it to use
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [System.Serializable]
        public struct AudioSet
        {
            public SFXSets SFX;
            public AudioClip[] Sounds;
        }
        public static AudioManager Instance;
        [SerializeField] private int _sourceCount;
        [SerializeField] private AudioSource _sourcePrefab;
        [SerializeField] private SerializedDictionary<SFXSets, AudioClip[]> _availableSets;
        Pooling<AudioSource> _pool;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _pool = new Pooling<AudioSource>(_sourcePrefab, _sourceCount, transform, 0);
        }

        /// <summary>
        /// Spawns a new audio source in the world
        /// </summary>
        /// <param name="sfx">Set from which audioclip will be pulled from</param>
        /// <param name="volume">Audio Source volume</param>
        /// <param name="position">Audio Source position in 3D world</param>
        /// <param name="pitchDefault">Audio Source pitch default value</param>
        /// <param name="pitchRange">Audio Source pitch variation range</param>
        public void PlaySound(SFXSets sfx, float volume, Vector3 position, float pitchDefault = 1, float pitchRange = 0)
        {
            AudioClip newClip = GetRandomClipFromSet(sfx);
            if (newClip == null)
            {
                Debug.LogWarning($"No audio clip found in set: {sfx}");
                return;
            }

            AudioSource pooledSource = _pool.GetFromPool(position, Quaternion.identity);
            if (pooledSource == null)
            {
                Debug.LogWarning("No available audio sources in the pool");
                return;
            }

            pooledSource.gameObject.SetActive(false);
            pooledSource.Stop();
            pooledSource.clip = newClip;
            pooledSource.volume = volume;
            pooledSource.pitch = pitchDefault + Random.Range(-pitchRange, pitchRange);
            pooledSource.gameObject.SetActive(true);
            pooledSource.Play();
            StartCoroutine(ReturnDelayedRoutine(pooledSource.transform, pooledSource.clip.length + 1.0f));
        }

        /// <summary>
        /// Gets a random audio clip from an audio set
        /// </summary>
        /// <param name="sfx">Name of the audio set</param>
        /// <returns>Random clip from audio set</returns>
        private AudioClip GetRandomClipFromSet(SFXSets sfx)
        {
            if (_availableSets.TryGetValue(sfx, out var clips))
            {
                if (clips.Length == 0)
                {
                    Debug.LogWarning($"Audio set '{sfx}' is empty");
                    return null;
                }

                int r = Random.Range(0, clips.Length);
                return clips[r];
            }

            Debug.LogWarning($"Audio set '{sfx}' not found");
            return null;
        }
        IEnumerator ReturnDelayedRoutine(Transform transform, float time)
        {
            yield return new WaitForSeconds(time);
            ReturnToPool(transform);
        }
        /// <summary>
        /// Return a pooled object to the pool.
        /// It is called by each Audio Source when needed
        /// </summary>
        /// <param name="t">Audio Source transform</param>
        public void ReturnToPool(Transform t)
        {
            AudioSource source = t.GetComponent<AudioSource>();
            if (source != null)
            {
                source.Stop();
                _pool.ReturnToPool(source);
            }
            else
            {
                Debug.LogWarning("Transform does not have an AudioSource component");
            }
        }
    }
}