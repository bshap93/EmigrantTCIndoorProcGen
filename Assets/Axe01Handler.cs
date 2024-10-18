using System.Collections;
using System.Collections.Generic;
using Audio.Sounds.Scripts;
using Characters.Scripts;
using Items.Equipment.Consumables;
using Items.Weapons;
using UnityEngine;

public class Axe01Handler : WeaponHandler 
{
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    public override void Use(IDamageable target)
    {
        throw new System.NotImplementedException();
    }
    public override void CeaseUsing()
    {
        throw new System.NotImplementedException();
    }
}
