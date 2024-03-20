using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedWeaponState : WeaponState
{
    public float MissileFlightSpeed { get; set; }
    public Projectile Projectile { get; set; }
    public GameObject Effect { get; set; }
    public GameObject ShootEffect { get; set; }
    public bool DestroyOnCollision { get; set; }
    public bool Reloaded { get; set; }

    public RangedWeaponState(RangedWeaponData rangedWeapon) : base(rangedWeapon)
    {
        MissileFlightSpeed = rangedWeapon.BaseMissileFlightSpeed;
        Projectile = rangedWeapon.Projectile;
        Effect = rangedWeapon.Effect;
        DestroyOnCollision=rangedWeapon.DestroyOnCollision;
        ShootEffect= rangedWeapon.ShootEffect;
    }
}
