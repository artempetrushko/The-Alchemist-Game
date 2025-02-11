namespace GameLogic.LootSystem
{
    public class StuffState : RangedWeaponState
    {
        public StickAttackType AttackType { get; set; }

        public StuffState(StuffData stick) : base(stick)
        {
            AttackType = stick.BaseAttackType;
        }

        public override object Clone() => new StuffState(BaseParams as StuffData)
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

            AttackType = AttackType,
        };
    }
}