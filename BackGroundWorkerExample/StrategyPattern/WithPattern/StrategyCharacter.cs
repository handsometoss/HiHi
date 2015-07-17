using System.Collections.Generic;
using BackGroundWorkerExample.StrategyPattern.WithPattern.ClassRace;

namespace BackGroundWorkerExample.StrategyPattern.WithPattern
{
    public class StrategyCharacter
    {
        private IDictionary<UnitType, IInitDamageUnit> initDamageUnit = new Dictionary<UnitType, IInitDamageUnit>
        {
            {UnitType.Assassin, new ProcessDamageAssassin()},
            {UnitType.Mage, new ProcessDamageMage()},
            {UnitType.Tanker, new ProcessDamageTanker()},
            {UnitType.Warrior, new ProcessDamageWarrior()},
            {UnitType.WitchDoctor, new ProcessDamageWitchDoctor()}
        };

        public void CastSkill(UnitType unitType)
        {
            var initDamage = initDamageUnit[unitType];
        }
    }
}
