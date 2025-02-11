namespace GameLogic.LootSystem
{
    public class MeleeWeaponState : WeaponState
    {
        public float BlockingEfficiency { get; set; }

        public MeleeWeaponState(MeleeWeaponData meleeWeapon) : base(meleeWeapon)
        {
            BlockingEfficiency = meleeWeapon.BaseBlockingEfficiency;
        }

        public override object Clone() => new MeleeWeaponState(BaseParams as MeleeWeaponData)
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

            BlockingEfficiency = BlockingEfficiency,
        };
    }
}