namespace BackGroundWorkerExample.StrategyPattern.WithoutPattern
{
    public class NoStrategyCharacter
    {
        private readonly InitDamageUnit _initDamageUnit = new InitDamageUnit();

        public void CastSkill(UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Assassin:
                    _initDamageUnit.ProcessDamageAssassin();
                    break;
                case UnitType.Mage:
                    _initDamageUnit.ProcessDamageMage();
                    break;
                case UnitType.Tanker:
                    _initDamageUnit.ProcessDamageTanker();
                    break;
                case UnitType.Warrior:
                    _initDamageUnit.ProcessDamageWarrior();
                    break;
                case UnitType.WitchDoctor:
                    _initDamageUnit.ProcessDamageWitchDoctor();
                    break;

            }
        }
    }
}
