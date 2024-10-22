using System.Collections.Generic;
using UnityEngine;

namespace Audio.Sounds.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Sound Effect", menuName = "Audio/Sound Effect")]
    public class SoundEffect : ScriptableObject
    {
        // List of audio clips that can be played
        public AudioClip[] clips;

        // Indicates if this sound effect should loop by default
        public bool isLooping;

        // Volume for this sound effect
        [Range(0f, 1f)] public float volume = 1f;

        int _currentClipIndex;

        List<int> _remainingClipIndices;

        public AudioClip GetRandomClip()
        {
            if (clips.Length == 0) return null;

            if (_remainingClipIndices == null || _remainingClipIndices.Count == 0) ResetClipIndices();

            var randomIndex = Random.Range(0, _remainingClipIndices.Count);
            var clipIndex = _remainingClipIndices[randomIndex];
            _remainingClipIndices.RemoveAt(randomIndex);

            return clips[clipIndex];
        }

        void ResetClipIndices()
        {
            _remainingClipIndices = new List<int>();
            for (var i = 0; i < clips.Length; i++) _remainingClipIndices.Add(i);
        }

        public AudioClip GetClipByIndex(int index)
        {
            if (clips.Length == 0) return null;
            return clips[Mathf.Clamp(index, 0, clips.Length - 1)];
        }

        public int GetClipCount()
        {
            return clips.Length;
        }

        public AudioClip GetNextClipInSequence()
        {
            if (clips.Length == 0) return null;
            if (_currentClipIndex >= clips.Length) _currentClipIndex = 0;
            return clips[_currentClipIndex++];
        }

        public AudioClip GetLoopingClip()
        {
            if (clips.Length == 0) return null;
            // For looping, we typically want to use the first clip
            // But you could modify this to use a specific clip for looping if needed
            return clips[0];
        }
    }
}
