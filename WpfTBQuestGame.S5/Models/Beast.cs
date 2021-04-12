using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class Beast : Npc, IBattle
    {
        #region PROPERTIES
        public int AttackLevel { get; set; }
        public BattleModeName BattleMode { get; set; }
        public Weapon CurrentWeapon { get; set; }
        protected override string InformationText()
        {
            return $"{Name} - {Description}";
        }
        #endregion

        #region CONSTUCTORS

        public Beast() { } //default constructor

        public Beast(int id, string name, int location, Genus kind, string description, bool isAvailable,  int attackLevel, Weapon currentWeapon)
            : base(id, name, location, kind, description, isAvailable)
        {
            AttackLevel = attackLevel;
            CurrentWeapon = currentWeapon;
        }

        #endregion

        #region METHODS
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
