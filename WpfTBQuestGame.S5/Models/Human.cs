using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class Human : Npc, IBattle, ISpeak
    {

        #region PROPERTIES

        public int AttackLevel { get; set; }
        public BattleModeName BattleMode { get; set; }
        public Weapon CurrentWeapon { get; set; }
        public List<string> Messages { get; set; }
        protected override string InformationText()
        {
            return $"{Name} - {Description}";
        }

        #endregion

        #region CONSTUCTORS

        public Human()
        {

        }
        public Human(int id, string name, int location, Genus kind, string description, bool isAvailable, List<string> messages, int attackLevel, Weapon currentWeapon) 
            : base (id, name, location, kind, description, isAvailable)
        {
            Messages = messages;
            AttackLevel = attackLevel;
            CurrentWeapon = currentWeapon;
        }

        #endregion

        #region METHODS

        public string Speak()
        {
            if (this.Messages != null)
            {
                return GetMessage();
            }
            else
            {
                return "";
            }
        }
        private string GetMessage()
        {
            Random r = new Random();
            int messageIndex = r.Next(0, Messages.Count());
            return Messages[messageIndex];
        }
        public int Attack()
        {
            int attackpoints = CurrentWeapon.AttackPoints * AttackLevel;
            return attackpoints;
        }
        public int Defend()
        {
            int attackpoints = CurrentWeapon.AttackPoints * AttackLevel;
            return attackpoints;
        }
        public int Retreat()
        {
            int attackpoints = CurrentWeapon.AttackPoints * AttackLevel;
            return attackpoints;
        }

        #endregion

    }
}
