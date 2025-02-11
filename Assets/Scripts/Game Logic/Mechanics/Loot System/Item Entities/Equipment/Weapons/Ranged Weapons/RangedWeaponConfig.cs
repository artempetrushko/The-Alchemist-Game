using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class RangedWeaponConfig : WeaponConfig
	{
		[SerializeField] private int _missileFlightSpeed;
        [SerializeField] private GameObject _effect;
        [SerializeField] private GameObject _shootEffect;
        [SerializeField] private bool _destroyOnCollision;

		public int BaseMissileFlightSpeed => _missileFlightSpeed;

		public GameObject Effect => _effect;
		public GameObject ShootEffect => _shootEffect;
		public bool DestroyOnCollision => _destroyOnCollision;
	}
}