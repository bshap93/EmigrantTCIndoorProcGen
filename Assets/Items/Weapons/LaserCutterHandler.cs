using System.Collections.Generic;
using Characters.Scripts;
using UnityEngine;

namespace Items.Weapons
{
    public class LaserCutterHandler : WeaponHandler
    {
        public GameObject hitEffect;
        public GameObject cutEffect;
        public LineRenderer lineRenderer;
        public override void Use(IDamageable target)
        {
            FireLaserCutter();
        }
        public override void CeaseUsing()
        {
            StartCoroutine(DisableLineRenderer());
        }


        void FireLaserCutter()
        {
            // Bit shift the index of the layer (8) to get a bit mask
            // int layerMask = 1 << 8;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            // layerMask = ~layerMask;


            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(
                    firePoint.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                lineRenderer.enabled = true;

                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, hit.point);


                Debug.DrawRay(
                    firePoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);


                HandleLasercutterHit(hit);
                if (hit.collider.isTrigger && hit.transform.gameObject.TryGetComponent(out ICuttable cuttable))
                {
                    cutEffect.transform.position = hit.point;
                    cutEffect.SetActive(true);
                    var secondsToCut = cuttable.GetSecondsToCut();
                    cuttable.Cut(secondsToCut);


                    StartCoroutine(DisableCutEffect(secondsToCut));
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            }
        }

        IEnumerator<WaitForSeconds> DisableCutEffect(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            cutEffect.SetActive(false);
        }

        IEnumerator<WaitForSeconds> DisableLineRenderer()
        {
            yield return new WaitForSeconds(0.1f);
            lineRenderer.enabled = false;
            hitEffect.SetActive(false);
        }

        void HandleLasercutterHit(RaycastHit hit)
        {
            hitEffect.transform.position = hit.point;
            hitEffect.SetActive(true);
            if (hit.transform.gameObject.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(damageable, 0);

            if (hit.transform.gameObject.TryGetComponent(out ICuttable cuttable))
                cuttable.Cut(cuttable.GetSecondsToCut());
        }
    }
}
