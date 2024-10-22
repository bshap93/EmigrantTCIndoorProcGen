using System.Collections.Generic;
using Audio.Sounds.Scripts;
using Environment.ObjectAttributes.Interfaces;
using UnityEngine;

namespace Items.Weapons
{
    public class LaserCutterHandler : WeaponHandler
    {
        public GameObject hitEffect;
        public GameObject cutEffect;
        public LineRenderer lineRenderer;
        AudioManager audioManager;
        ICuttable currentCuttable;
        bool isUsing;

        void Start()
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
        public override void Use(IDamageable target)
        {
            FireLaserCutter();
        }
        public override void CeaseUsing()
        {
            StartCoroutine(DisableLineRenderer());
            if (isUsing) audioManager.OnStopLoopingEffect.Invoke("LaserSoundEffect");
            isUsing = false;
        }


        void FireLaserCutter()
        {
            // Bit shift the index of the layer (8) to get a bit mask
            // int layerMask = 1 << 8;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            // layerMask = ~layerMask;

            if (!isUsing) audioManager.OnPlayLoopingEffect.Invoke("LaserSoundEffect", firePoint.position, true);


            Debug.Log("Firing laser cutter");
            isUsing = true;


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
            }
            else
            {
                StopCutting();
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            }
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
            {
                if (cuttable != currentCuttable)
                {
                    StopCutting();
                    currentCuttable = cuttable;
                }

                if (hit.collider.isTrigger)
                {
                    cutEffect.transform.position = hit.point;
                    cutEffect.SetActive(true);
                }

                cuttable.Cut(Time.deltaTime);
            }
            else
            {
                StopCutting();
            }
        }

        void StopCutting()
        {
            if (currentCuttable != null)
            {
                cutEffect.SetActive(false);
                currentCuttable = null;
            }
        }
    }
}
