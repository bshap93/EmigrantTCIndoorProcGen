using System.Collections.Generic;
using Environment.ObjectAttributes.Interfaces;
using UnityEngine;

namespace Items.Weapons
{
    public class FireExtinguisherHandler : WeaponHandler
    {
        public GameObject hitEffect;
        public GameObject extinguishEffect;
        public ParticleSystem extinguisherParticleSystem;
        public override void Use(IDamageable target)
        {
            FireFireExtinguisher();
        }
        public override void CeaseUsing()
        {
            StartCoroutine(DisableParticleSystem());
        }

        void FireFireExtinguisher()
        {
            RaycastHit hit;
            if (Physics.Raycast(
                    firePoint.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                extinguisherParticleSystem.Play();
                Debug.DrawRay(
                    firePoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);


                HandleFireExtinguisherHit(hit);
                if (hit.collider.isTrigger &&
                    hit.transform.gameObject.TryGetComponent(out IExtinguishable extinguishable))
                {
                    extinguishEffect.transform.position = hit.point;
                    extinguishEffect.SetActive(true);
                    var secondsToExtinguish = extinguishable.GetSecondsToExtinguish();
                    extinguishable.Extinguish(secondsToExtinguish);
                    StartCoroutine(DisableExtinguishEffect());
                }
            }
        }
        IEnumerator<WaitForSeconds> DisableExtinguishEffect()
        {
            yield return new WaitForSeconds(0.1f);
            extinguishEffect.SetActive(false);
        }

        IEnumerator<WaitForSeconds> DisableParticleSystem()
        {
            yield return new WaitForSeconds(0.1f);
            extinguisherParticleSystem.Stop();
            hitEffect.SetActive(false);
        }
        void HandleFireExtinguisherHit(RaycastHit hit)
        {
            hitEffect.transform.position = hit.point;
            extinguishEffect.transform.position = hit.point;
            hitEffect.SetActive(true);
            extinguishEffect.SetActive(true);


            if (hit.transform.gameObject.TryGetComponent(out IExtinguishable extinguishable))
                extinguishable.Extinguish(extinguishable.GetSecondsToExtinguish());
        }
    }
}
