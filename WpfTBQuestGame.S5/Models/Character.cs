using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class Character : ObservableObject
    {
        //Humans should have the option to trade, talk, battle, gift items, assign quest
        //Creatures can battle, talk, gift items, assign quest (no trade, gift after quest or defeated)
        //Beasts battle, gift items (no talk, no quest, no trade, reveal gift when defeated)

        #region ENUMS

        public enum Genus
        {
            Player,
            Human,
            Creature,
            Beast
        }

        #endregion

        #region PROPERTIES

        //random object
        protected Random random = new Random();
        public int Id { get; set; }
        public string Name { get; set; }
        public int Location { get; set; }
        public Genus Kind { get; set; }

        #endregion

        #region CONSTRUCTORS

        public Character()
        {
        }

        public Character(int id, string name, int location, Genus kind)
        {
            Id = id;
            Name = name;
            Location = location;
            Kind = kind;

        }

        #endregion

        public virtual string CharacterMessage(string characterMessage)
        {
            return characterMessage;
        }
    }
}
