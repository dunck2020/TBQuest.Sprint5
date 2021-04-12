using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public abstract class Npc : Character
    {
        #region PROPERTIES
        public ObservableCollection<GameItem> Inventory { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public string Information
        {
            get
            {
                return InformationText();
            }
            set
            {

            }
        }

        #endregion

        #region CONSTRUCTORS

        public Npc () {} // default constructor

        public Npc(int id, string name, int location, Genus kind, string description, bool isAvailable)
            : base (id, name, location, kind)
        {
            Id = id;
            Name = name;
            Kind = kind;
            Location = location;
            Description = description;
            IsAvailable = isAvailable;
        }

        #endregion

        #region METHODS

        protected abstract string InformationText();

        public void RemoveGameItemFromNpc(GameItem gameItem)
        {
            Inventory.Remove(gameItem);
        }
        #endregion
    }
}
