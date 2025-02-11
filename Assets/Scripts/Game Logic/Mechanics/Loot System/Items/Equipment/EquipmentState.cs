namespace GameLogic.LootSystem
{
    public abstract class EquipmentState : ItemState
    {
        public int Endurance { get; set; }
        public int MaxEndurance { get; set; }

        public EquipmentState(EquipmentData equipment) : base(equipment)
        {
            Endurance = equipment.BaseEndurance;
            MaxEndurance = equipment.BaseEndurance;
        }
    }
}