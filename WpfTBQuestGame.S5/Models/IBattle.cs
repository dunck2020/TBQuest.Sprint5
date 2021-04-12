using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    interface IBattle
    {
        int AttackLevel { get; set; }
        Weapon CurrentWeapon { get; set; }
        BattleModeName BattleMode { get; set; }

        int Attack();
        int Defend();
        int Retreat();

    }
}
