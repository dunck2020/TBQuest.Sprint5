using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class Artifact : GameItem
    {
        public enum UseActionType
        {
            OPEN_LOCATION,
            KILL_PLAYER,
            HORSE_NO_KILL,
            ABYSS_NO_KILL,
            SOULS_NO_HEALTH_LOSS,
            INHUMAN_CLASS_OPEN,
            HEAL_PLAYER
        }
        public UseActionType UseAction { get; set; }

        public Artifact(int id, string name, int value, string description, string useMessage, UseActionType useAction, bool isAvailable) :
            base(id, name, value, description, useMessage, isAvailable)
        {
            UseAction = useAction;
        }
        public override string InformationString()
        {
            return $"{Name}: {Description}\nValue: {Value}";
        }
    }
}
