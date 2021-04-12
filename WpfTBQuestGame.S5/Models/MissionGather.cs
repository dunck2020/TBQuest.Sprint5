using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class MissionGather : Mission
    {
        private int _id;
        private string _name;
        private string _description;
        private MissionStatus _status;
        private string _statusDetail;
        private int _experiencePoints;
        public List<GameItem> RequiredGameItems { get; set; }

        public MissionGather() { } // default

        public MissionGather(int id, string name, MissionStatus status, List<GameItem> requiredGameItems)
            : base(id, name, status)
        {
            _id = id;
            _name = name;
            _status = status;
            RequiredGameItems = requiredGameItems;
        }

        public List<GameItem> GameItemsNotCompleted(List<GameItem> inventory)
        {
            List<GameItem> gameItemsToComplete = new List<GameItem>();

            foreach (var missionGameItem in RequiredGameItems)
            {
                GameItem inventoryItemMatch = inventory.FirstOrDefault(gi => gi.Id == missionGameItem.Id);

                if (inventoryItemMatch == null) gameItemsToComplete.Add(missionGameItem);
            }

            return gameItemsToComplete;
        }
    }
}
