using Audio.Sounds.Scripts;
using Core.Events;
using UnityEngine;

namespace Audio.Sounds.Player.Scripts
{
    public class CharacterFootsteps : MonoBehaviour
    {
        public float stepInterval = 0.5f; // Time between steps
        AudioManager _audioManager;

        void Start()
        {
            _audioManager = FindObjectOfType<AudioManager>();
            EventManager.EOnCharacterIsMoving.AddListener(OnCharacterStartMoving);
            EventManager.EOnCharacterStoppedMoving.AddListener(OnCharacterStopMoving);
        }

        void OnCharacterStartMoving(float speed)
        {
            PlayFootsteps();
        }

        void OnCharacterStopMoving()
        {
            StopFootsteps();
        }

        public void PlayFootsteps()
        {
            InvokeRepeating(nameof(PlayFootstepSound), 0, stepInterval);
        }

        public void StopFootsteps()
        {
            CancelInvoke(nameof(PlayFootstepSound));
        }

        void PlayFootstepSound()
        {
            var surfaceType = DetermineSurfaceType();
            var footstepSoundName = $"Footstep_{surfaceType}";

            // Use your AudioManager to play the footstep sound
            _audioManager.PlayEffect(footstepSoundName);
        }

        string DetermineSurfaceType()
        {
            // This method would use raycasts or other logic to determine what surface the character is on
            // For this example, we'll just return a default surface
            return "Bulkhead";
        }
    }
}
