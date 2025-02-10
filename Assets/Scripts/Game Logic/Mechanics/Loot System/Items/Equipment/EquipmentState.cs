namespace GameLogic.LootSystem
{
    public abstract class EquipmentState : ItemState
    {
        public int Endurance { get; set; }
        public int MaxEndurance { get; set; }
        public int MaxRuneSize { get; set; }
        public int PoweredEnergyCount { get; set; }
        public int EnergyCapacity { get; set; }
        public RuneState ImposedRune { get; set; }

        public EquipmentState(EquipmentData equipment) : base(equipment)
        {
            Endurance = equipment.BaseEndurance;
            MaxEndurance = equipment.BaseEndurance;
            MaxRuneSize = equipment.BaseMaxRuneSize;
            PoweredEnergyCount = equipment.BasePoweredEnergyCount;
            EnergyCapacity = equipment.BaseEnergyCapacity;
        }
    }
}