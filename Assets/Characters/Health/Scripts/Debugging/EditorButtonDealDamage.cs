using Characters.Player.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Health.Scripts.Debugging
{
    public class EditorButtonDealDamage : MonoBehaviour
    {
        // Button that triggers the UnityEvent
        [FormerlySerializedAs("player")]
        [FormerlySerializedAs("playerStateController")]
        [Button("Manually Deal Damage (Debug)")]
        public PlayerCharacter playerCharacter;
        public void DebugDealDamage()
        {
        }
    }
}
