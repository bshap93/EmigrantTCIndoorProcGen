using System.Collections.Generic;
using Audio.Sounds.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Audio.Sounds.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        [FormerlySerializedAs("SoundEffects")] public SoundEffect[] soundEffects;
        AudioListener _audioListener;
        Dictionary<string, SoundEffect> _soundEffectDictionary;

        void Awake()
        {
            _soundEffectDictionary = new Dictionary<string, SoundEffect>();
            foreach (var soundEffect in soundEffects)
            {
                Debug.LogFormat("registered effect {0}", soundEffect.name);
                _soundEffectDictionary[soundEffect.name] = soundEffect;
            }

            // Don't destroy the audio manager when loading a new scene
            DontDestroyOnLoad(gameObject);
        }

        public void PlayEffect(string effectName)
        {
            if (_audioListener == null) _audioListener = FindObjectOfType<AudioListener>();

            PlayEffect(effectName, _audioListener.transform.position);
        }

        public void PlayEffect(string effectName, Vector3 worldPosition)
        {
            if (!_soundEffectDictionary.ContainsKey(effectName))
            {
                Debug.LogErrorFormat("Sound effect {0} not registered", effectName);
                return;
            }

            var clip = _soundEffectDictionary[effectName].GetRandomClip();

            if (clip == null)
            {
                Debug.LogErrorFormat("No clips found for sound effect {0}", effectName);
                return;
            }

            AudioSource.PlayClipAtPoint(clip, worldPosition);
        }
    }
}
