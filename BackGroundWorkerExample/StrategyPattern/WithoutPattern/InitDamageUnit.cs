using System.Windows;

namespace BackGroundWorkerExample.StrategyPattern.WithoutPattern
{
    public class InitDamageUnit
    {
        public void ProcessDamageAssassin()
        {
            MessageBox.Show("The Assassin cast backstab deal 11.00 damage");
        }

        public void ProcessDamageMage()
        {
            MessageBox.Show("The Mage cast Firebolt deal 9.99 damage");
        }

        public void ProcessDamageTanker()
        {
            MessageBox.Show("The Tanker cast Provoke to the enemies");
        }

        public void ProcessDamageWarrior()
        {
            MessageBox.Show("The Warrior cast TerraBreak deal 12.00 damage");
        }

        public void ProcessDamageWitchDoctor()
        {
            MessageBox.Show("The Witcher Doctor cast cursed pupet dealt 8.00 AOE damage");
        }
    }
}