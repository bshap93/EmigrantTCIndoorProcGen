using System;
using System.Collections.Generic;
using Audio.Sounds.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Audio.Sounds.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        // Static events
        public static PlayEffectEvent OnPlayEffect = new();
        public static PlayEffectAtPointEvent OnPlayEffectAtPoint = new();
        public static PlayLoopingEffectEvent OnPlayLoopingEffect = new();
        public static PlayEffectEvent OnStopLoopingEffect = new();

        static AudioManager _instance;

        public SoundEffect[] soundEffects;

        // Instance events (for Inspector assignments)
        public PlayEffectEvent onPlayEffect;
        public PlayEffectAtPointEvent onPlayEffectAtPoint;
        public PlayLoopingEffectEvent onPlayLoopingEffect;
        public PlayEffectEvent onStopLoopingEffect;
        AudioListener _audioListener;
        Dictionary<string, AudioSource> _loopingSources;
        Dictionary<string, SoundEffect> _soundEffectDictionary;

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeSoundEffects();
                _loopingSources = new Dictionary<string, AudioSource>();
                LinkEvents();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void LinkEvents()
        {
            OnPlayEffect.AddListener(PlayEffect);
            OnPlayEffectAtPoint.AddListener(PlayEffect);
            OnPlayLoopingEffect.AddListener(PlayLoopingEffect);
            OnStopLoopingEffect.AddListener(StopLoopingEffect);

            onPlayEffect.AddListener(PlayEffect);
            onPlayEffectAtPoint.AddListener(PlayEffect);
            onPlayLoopingEffect.AddListener(PlayLoopingEffect);
            onStopLoopingEffect.AddListener(StopLoopingEffect);
        }

        void InitializeSoundEffects()
        {
            _soundEffectDictionary = new Dictionary<string, SoundEffect>();
            foreach (var soundEffect in soundEffects)
            {
                Debug.LogFormat("Registered effect {0}", soundEffect.name);
                _soundEffectDictionary[soundEffect.name] = soundEffect;
            }
        }

        public void PlayEffect(string effectName)
        {
            if (_audioListener == null) _audioListener = FindObjectOfType<AudioListener>();
            PlayEffect(effectName, _audioListener.transform.position);
        }

        public void PlayEffect(string effectName, Vector3 worldPosition)
        {
            if (!_soundEffectDictionary.TryGetValue(effectName, out var soundEffect))
            {
                Debug.LogErrorFormat("Sound effect {0} not registered", effectName);
                return;
            }

            var clip = soundEffect.isLooping ? soundEffect.GetLoopingClip() : soundEffect.GetRandomClip();
            if (clip == null)
            {
                Debug.LogErrorFormat("No clips found for sound effect {0}", effectName);
                return;
            }

            AudioSource.PlayClipAtPoint(clip, worldPosition);

            if (soundEffect.isLooping && !_loopingSources.ContainsKey(effectName))
            {
                var newSource = new GameObject($"LoopingAudio_{effectName}").AddComponent<AudioSource>();
                newSource.clip = clip;
                newSource.loop = true;
                newSource.volume = soundEffect.volume;
                newSource.transform.position = worldPosition;
                newSource.Play();
                _loopingSources[effectName] = newSource;
            }
            else if (_loopingSources.TryGetValue(effectName, out var existingSource))
            {
                existingSource.loop = soundEffect.isLooping;
                existingSource.volume = soundEffect.volume;
            }
        }

        public void PlayLoopingEffect(string effectName, Vector3 worldPosition, bool loop)
        {
            if (!_soundEffectDictionary.TryGetValue(effectName, out var soundEffect))
            {
                Debug.LogErrorFormat("Sound effect {0} not registered", effectName);
                return;
            }

            var clip = soundEffect.GetRandomClip();
            if (clip == null)
            {
                Debug.LogErrorFormat("No clips found for sound effect {0}", effectName);
                return;
            }

            if (_loopingSources.TryGetValue(effectName, out var existingSource))
            {
                existingSource.Stop();
                existingSource.clip = clip;
                existingSource.loop = loop;
                existingSource.Play();
                existingSource.transform.position = worldPosition;
            }
            else
            {
                var audioSource = new GameObject($"LoopingAudio_{effectName}").AddComponent<AudioSource>();
                audioSource.clip = clip;
                audioSource.loop = loop;
                audioSource.Play();
                audioSource.transform.position = worldPosition;
                _loopingSources[effectName] = audioSource;
            }
        }

        public void StopLoopingEffect(string effectName)
        {
            if (_loopingSources.TryGetValue(effectName, out var audioSource))
            {
                audioSource.Stop();
                Destroy(audioSource.gameObject);
                _loopingSources.Remove(effectName);
            }
        }

        [Serializable]
        public class PlayEffectEvent : UnityEvent<string>
        {
        }

        [Serializable]
        public class PlayEffectAtPointEvent : UnityEvent<string, Vector3>
        {
        }

        [Serializable]
        public class PlayLoopingEffectEvent : UnityEvent<string, Vector3, bool>
        {
        }
    }
}
