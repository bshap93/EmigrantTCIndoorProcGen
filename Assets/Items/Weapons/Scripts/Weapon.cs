using System;
using Characters.Enemies.Attacks.Commands;
using Environment.ObjectAttributes.Interfaces;
using Items.Equipment;
using Polyperfect.Crafting.Integration;

namespace Items.Weapons.Scripts
{
    public abstract class Weapon : BaseItemObject
    {
        public IAttackCommand AttackCommand;

        public abstract void InitializeAttackCommand(WeaponHandler weaponHandler);


        public virtual void Attack(IDamageable target, WeaponHandler weaponHandler)
        {
            throw new NotImplementedException();
        }

        public IAttackCommand GetAttackCommand()
        {
            return AttackCommand;
        }

        public void SetAttackCommand(IAttackCommand command)
        {
            AttackCommand = command;
        }

        public void InitializeUseCommand(EquippableHandler equippableHandler)
        {
            if (equippableHandler is WeaponHandler handler)
                InitializeAttackCommand(handler);
        }
    }
}
