using Audio.Sounds.Scripts;
using Environment.ObjectAttributes.Interfaces;
using UnityEngine;

namespace Items.Weapons
{
    public class AxeHandler : WeaponHandler
    {
        public float checkRadius = 0.5f; // Adjust based on your axe's size

        AudioManager audioManager;

        IBreakable currentBreakable;
        GameObject hitEffect;

        bool isSwinging;

        void Start()
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        // Optional: Visualize the check sphere in the editor
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, checkRadius);
        }
        public override void Use(IDamageable target)
        {
            SwingAxeAtForwardTarget();
        }
        bool SwingAxeAtForwardTarget()
        {
            Debug.Log("Swinging axe");
            isSwinging = true;
            // audioManager.OnPlayEffect.Invoke("AxeSwing");

            var hitColliders = Physics.OverlapSphere(firePoint.position, 1f);
            foreach (var hitCollider in hitColliders)
                if (hitCollider.gameObject.TryGetComponent(out IBreakable breakable))
                {
                    breakable.Break();
                    // breakEffect.SetActive(true);
                    return true;
                }

            return false;
        }


        public override void CeaseUsing()
        {
            isSwinging = false;
        }
    }
}
