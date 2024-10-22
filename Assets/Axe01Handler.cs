using System;
using Audio.Sounds.Scripts;
using Environment.ObjectAttributes.Interfaces;
using Items.Weapons;

public class Axe01Handler : WeaponHandler
{
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
    }

    public override void Use(IDamageable target)
    {
        throw new NotImplementedException();
    }
    public override void CeaseUsing()
    {
        throw new NotImplementedException();
    }
}
