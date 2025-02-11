namespace GameLogic.LootSystem
{
    public class CrossbowState : RangedWeaponState
    {
        public CrossbowState(CrossbowData crossbow) : base(crossbow) { }

        public override object Clone() => new CrossbowState(BaseParams as CrossbowData)
        {
            //ItemData = ItemData,
            Description = Description,

            Endurance = Endurance,

            Damage = Damage,
            Range = Range,
            Accuracy = Accuracy,
            AttackSpeed = AttackSpeed,
            CooldownTime = CooldownTime,
            PenetratingPower = PenetratingPower,

            MissileFlightSpeed = MissileFlightSpeed,
        };
    }
}