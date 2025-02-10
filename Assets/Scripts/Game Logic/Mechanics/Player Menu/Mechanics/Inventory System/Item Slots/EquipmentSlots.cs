using Controls;
using GameLogic.LootSystem;

namespace GameLogic.PlayerMenu.Inventory
{
    public class EquipmentSlots : ItemSlotCollection, ISelectableCollection
    {
        public WeaponSlot LeftWeaponSlot;
        public WeaponSlot RightWeaponSlot;
        public ClothesSlot HatSlot;
        public ClothesSlot RaincoatSlot;
        public ClothesSlot BootsSlot;
        public ClothesSlot GlovesSlot;
        public ClothesSlot MedallionSlot;

        public PlayerInputActionMap InputActionMap => throw new System.NotImplementedException();

        public IPlayerMenuInteractable GetStartSelectedElement(PlayerMenuNavigationStartCondition startCondition = PlayerMenuNavigationStartCondition.Default)
        {
            throw new System.NotImplementedException();
        }

        public bool TryPlaceItem(Item item)
        {
            return false;
        }
    }
}