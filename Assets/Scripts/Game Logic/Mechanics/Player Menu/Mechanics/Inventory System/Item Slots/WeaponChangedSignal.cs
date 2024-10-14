using GameLogic.LootSystem;

namespace GameLogic.PlayerMenu.Inventory
{
    public class WeaponChangedSignal
    {
        public readonly WeaponState Weapon;

        public WeaponChangedSignal(WeaponState weapon)
        {
            Weapon = weapon;
        }
    }
}