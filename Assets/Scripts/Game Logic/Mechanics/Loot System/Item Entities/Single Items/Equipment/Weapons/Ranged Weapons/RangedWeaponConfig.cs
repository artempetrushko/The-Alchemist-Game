using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class RangedWeaponConfig : WeaponConfig
	{
		[SerializeField] protected int missileFlightSpeed;
		[SerializeField] protected Projectile projectile;
		[SerializeField] protected GameObject effect;
		[SerializeField] protected GameObject shootEffect;
		[SerializeField] protected bool destroyOnCollision;

		public int BaseMissileFlightSpeed => missileFlightSpeed;

		public Projectile Projectile => projectile;
		public GameObject Effect => effect;
		public GameObject ShootEffect => shootEffect;
		public bool DestroyOnCollision => destroyOnCollision;
	}
}