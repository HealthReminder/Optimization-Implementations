using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Colosso.Tools.Audio
{
    [System.Serializable]
    public class SoundtrackSet
    {
        public string name;
        public List<AudioClip> audioClips;
    }

    public class SoundtrackManager : MonoBehaviour
    {
        public static SoundtrackManager Instance { get; private set; }

        public List<SoundtrackSet> soundtrackSets;
        public float crossfadeDuration = 1.0f;

        [SerializeField] private AudioSource[] audioSources;
        private int activeSourceIndex;
        private bool isCrossfading;
        private Dictionary<string, SoundtrackSet> soundtrackDictionary;
        private Queue<AudioClip> trackHistory = new Queue<AudioClip>();

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            activeSourceIndex = 0;
            isCrossfading = false;

            soundtrackDictionary = new Dictionary<string, SoundtrackSet>();
            foreach (var set in soundtrackSets)
            {
                soundtrackDictionary[set.name] = set;
            }
        }

        public void PlayRandomTrack(string setName, bool enableLooping = true)
        {
            if (!soundtrackDictionary.TryGetValue(setName, out SoundtrackSet set))
            {
                Debug.LogWarning("Soundtrack set not found: " + setName);
                return;
            }
            if (set.audioClips.Count == 0)
            {
                Debug.LogWarning("Soundtrack set empty: " + setName);
                return;
            }

            AudioClip randomClip = GetRandomClip(set);

            if (isCrossfading)
            {
                StopAllCoroutines();
            }
            StartCoroutine(CrossfadeToClip(randomClip, enableLooping));
        }

        private AudioClip GetRandomClip(SoundtrackSet set)
        {
            List<AudioClip> availableClips = new List<AudioClip>(set.audioClips);
            int maxAttempts = availableClips.Count; // Limit attempts to prevent infinite loop
            int attemptCount = 0;

            while (attemptCount < maxAttempts)
            {
                AudioClip randomClip = availableClips[Random.Range(0, availableClips.Count)];

                // Check if the clip is not in history or if history is empty
                if (!trackHistory.Contains(randomClip) || trackHistory.Count == 0)
                {
                    trackHistory.Enqueue(randomClip);
                    if (trackHistory.Count > 10)
                    {
                        trackHistory.Dequeue();
                    }
                    return randomClip;
                }

                attemptCount++;
            }

            // Fallback to return a random clip even if it's in history (if maxAttempts reached)
            return availableClips[Random.Range(0, availableClips.Count)];
        }


        private IEnumerator CrossfadeToClip(AudioClip newClip, bool enableLooping)
        {
            isCrossfading = true;

            int newSourceIndex = (activeSourceIndex + 1) % audioSources.Length;
            AudioSource newSource = audioSources[newSourceIndex];

            newSource.clip = newClip;
            newSource.loop = enableLooping;
            newSource.Play();

            float timer = 0f;
            AudioSource oldSource = audioSources[activeSourceIndex];
            while (timer < crossfadeDuration)
            {
                timer += Time.deltaTime;
                oldSource.volume = Mathf.Lerp(1f, 0f, timer / crossfadeDuration);
                newSource.volume = Mathf.Lerp(0f, 1f, timer / crossfadeDuration);
                yield return null;
            }

            oldSource.Stop();
            activeSourceIndex = newSourceIndex;
            isCrossfading = false;
        }

        public void SetVolume(float volume)
        {
            foreach (var source in audioSources)
            {
                source.volume = volume;
            }
        }

        public void Pause()
        {
            foreach (var source in audioSources)
            {
                source.Pause();
            }
        }

        public void Resume()
        {
            foreach (var source in audioSources)
            {
                source.UnPause();
            }
        }

        public void Stop()
        {
            if (isCrossfading)
            {
                StopAllCoroutines();
            }
            StartCoroutine(FadeOutAndStop());
        }

        private IEnumerator FadeOutAndStop()
        {
            float startVolume = audioSources[activeSourceIndex].volume;
            float timer = 0f;
            while (timer < crossfadeDuration)
            {
                timer += Time.deltaTime;
                foreach (var source in audioSources)
                {
                    source.volume = Mathf.Lerp(startVolume, 0f, timer / crossfadeDuration);
                }
                yield return null;
            }
            foreach (var source in audioSources)
            {
                source.Stop();
            }
        }

        public string GetCurrentTrackName()
        {
            return audioSources[activeSourceIndex].clip != null ? audioSources[activeSourceIndex].clip.name : "No track playing";
        }
    }
}