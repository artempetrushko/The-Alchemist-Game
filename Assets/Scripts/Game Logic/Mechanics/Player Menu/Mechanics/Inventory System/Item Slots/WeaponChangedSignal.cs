using GameLogic.LootSystem;

namespace GameLogic.PlayerMenu.Inventory
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