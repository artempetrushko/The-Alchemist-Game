using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class RangedWeaponState : WeaponState
    {
        public float MissileFlightSpeed { get; set; }
        public GameObject Effect { get; set; }
        public GameObject ShootEffect { get; set; }
        public bool DestroyOnCollision { get; set; }
        public bool Reloaded { get; set; }

        public RangedWeaponState(RangedWeaponData rangedWeapon) : base(rangedWeapon)
        {
            MissileFlightSpeed = rangedWeapon.BaseMissileFlightSpeed;
            Effect = rangedWeapon.Effect;
            DestroyOnCollision = rangedWeapon.DestroyOnCollision;
            ShootEffect = rangedWeapon.ShootEffect;
        }
    }
}