using GameLogic.LootSystem;

namespace EventBus
{
    public class WeaponChangedSignal
    {
        public readonly Weapon Weapon;

        public WeaponChangedSignal(Weapon weapon)
        {
            Weapon = weapon;
        }
    }
}