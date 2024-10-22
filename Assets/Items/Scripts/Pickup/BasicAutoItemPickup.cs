using Polyperfect.Crafting.Demo;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items.Scripts.Pickup
{
    public class BasicAutoItemPickup : MonoBehaviour
    {
        [FormerlySerializedAs("Interactor")] public GameObject interactor;
        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GatherInteractable gatherable))
            {
                
                gatherable.BeginInteract(interactor);
            }
            
        }
    }
}
