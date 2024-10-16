using UnityEngine;

namespace Audio.Sounds.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Sound Effect", menuName = "Audio/Sound Effect")]
    public class SoundEffect : ScriptableObject
    {
        // List of audio clips that can be played
        public AudioClip[] clips;

        int _currentClipIndex;

        public AudioClip GetRandomClip()
        {
            return clips[Random.Range(0, clips.Length)];
        }

        public AudioClip GetClipByIdex(int index)
        {
            return clips[index];
        }

        public int GetClipCount()
        {
            return clips.Length;
        }

        public AudioClip GetNextClipInSequence()
        {
            if (_currentClipIndex >= clips.Length) _currentClipIndex = 0;

            return clips[_currentClipIndex++];
        }
    }
}
