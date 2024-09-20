using System.Collections.Generic;
using Characters.Enemies;
using Characters.Scripts;
using Combat.Attacks.Commands;
using UnityEngine;

namespace Combat.Weapons.Scripts.PlayerWeapons
{
    public class LaserTool : Weapon
    {
        public LineRenderer lineRenderer;
        public Transform firePoint;
        public float laserRange = 10f;
        public float laserDuration = 0.1f;

        void Start()
        {
            range = laserRange;
            AttackCommand = new RangedAttackCommand(this, range, firePoint);
        }

        public override void Attack(Enemy target)
        {
            FireLaserTool();
        }

        void FireLaserTool()
        {
            StartCoroutine(LaserEffect());

            RaycastHit hit;
            if (Physics.Raycast(
                    firePoint.position,
                    firePoint.forward, out hit, laserRange,
                    ~0, QueryTriggerInteraction.Ignore))
            {
                var damageable = hit.transform.GetComponent<IDamageable>();
                if (damageable != null)
                    Debug.Log("Hit: " + hit.transform.name);

                if (damageable != null) damageable.TakeDamage(damageable, damage);

                // Set the Line Renderer positions (from the fire point to the hit point)
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                // If the laser doesn't hit anything, set the line to max range
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, firePoint.position + firePoint.forward * laserRange);
            }
        }

        IEnumerator<WaitForSeconds> LaserEffect()
        {
            lineRenderer.widthMultiplier = 0.1f;
            lineRenderer.enabled = true;
            yield return new WaitForSeconds(laserDuration);
            lineRenderer.enabled = false;
        }
    }
}
