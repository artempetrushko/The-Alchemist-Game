using GameLogic.EnvironmentExploration;
using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class RangedWeapon : Weapon
    {
        public float MissileFlightSpeed { get; set; }
        public GameObject Effect { get; set; }
        public GameObject ShootEffect { get; set; }
        public bool DestroyOnCollision { get; set; }
        public bool Reloaded { get; set; }

        public RangedWeapon(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
        {
        }
    }
}