using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class Spell : GameItem
    {
        public int HealthChange { get; set; }
        public int LivesChange { get; set; }
        public int AttackPointsChange { get; set; }

        public Spell(int id, string name, int value, string description, string useMessage, int healthChange, int livesChange, int attackPointsChange,bool isAvailable) :
            base (id, name, value, description,  useMessage, isAvailable)
        {
            HealthChange = healthChange;
            LivesChange = livesChange;
            AttackPointsChange = attackPointsChange;
        }

        public override string InformationString()
        {
            if (HealthChange != 0)
            {
                return $"{Name}: {Description}\nHealth: {HealthChange}";
            }
            else if (LivesChange != 0)
            {
                return $"{Name}: {Description}\nLives: {LivesChange}";
            }
            else if (AttackPointsChange != 0)
            {
                return $"{Name}: {Description}\nAttack Points: {AttackPointsChange}";
            }
            else
            {
                return $"{Name}: {Description}";
            }
        }
    }
}
