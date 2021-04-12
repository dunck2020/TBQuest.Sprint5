using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class Treasure : GameItem
    {
        public enum TreasureType
        {
            Ingot,
            Gem,
            Scroll,
            General
        }
        public TreasureType Type { get; set; }

        public Treasure(int id, string name, int value, string description, string useMessage, TreasureType type, bool isAvailable) :
            base(id, name, value, description, useMessage, isAvailable)
        {
            Type = type;
        }
        public override string InformationString()
        {
            return $"{Name}: {Description}\nValue: {Value}";
        }
    }
}
