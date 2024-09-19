﻿using System.Collections.Generic;
using Characters.Enemies;
using Combat.Attacks.Commands;
using UnityEngine;

namespace Combat.Weapons.Scripts
{
    public class LaserTool : Weapon
    {
        public LineRenderer lineRenderer;
        public Transform firePoint;
        public float laserWidth = 0.1f;
        public float laserRange = 10f;

        void Start()
        {
            damage = 10f;
            range = laserRange;
            AttackCommand = new RangedAttackCommand(this, range, firePoint);
        }

        public override void Attack(Enemy target)
        {
            Debug.Log("LaserTool: Attack");
            FireLaserTool();
        }
        
        void FireLaserTool()
        {

        }
        
        IEnumerator<WaitForSeconds> LaserEffect()
        {
            
            yield return new WaitForSeconds(0.1f);
        }
    }
}
