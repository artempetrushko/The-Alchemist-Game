using GameLogic.EnvironmentExploration;
using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class Weapon : Equipment
    {
        public ItemParameter<int> Damage;
        public ItemParameter<float> Range;
        public ItemParameter<float> Accuracy;
        public ItemParameter<float> AttackSpeed;
        public ItemParameter<float> CooldownTime;
        public ItemParameter<int> PenetratingPower;
        public ABC_Controller.Weapon LinkedABCWeapon;
        public AnimatorOverrideController Animations;

        public Weapon(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
        {
        }
    }
}