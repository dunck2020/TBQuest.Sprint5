using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class Weapon : GameItem
    {

        #region PROPERTIES

        public bool Bindable { get; set; }
        public int AttackPoints { get; set; }
        public int TimesCanUse { get; set; }
        public double EnemyHitsPerUse { get; set; }

        #endregion

        #region CONSTUCTORS

        public Weapon(  int id, string name, int value, string description, string useMessage,   //base properties
                        bool bindable, int attackPoints, int timesCanUse, double enemyHitsPerUse, bool isAvailable)  //weapon properties
            : base (id, name, value, description, useMessage, isAvailable)
        {
            Bindable = bindable;
            AttackPoints = attackPoints;
            TimesCanUse = timesCanUse;
            EnemyHitsPerUse = enemyHitsPerUse;
            IsAvailable = isAvailable;
        }

        #endregion

        #region METHODS

        public override string InformationString()
        {
            return $"{Name}: {Description} \nAttack Points: {AttackPoints}";
        }

        #endregion

    }
}
