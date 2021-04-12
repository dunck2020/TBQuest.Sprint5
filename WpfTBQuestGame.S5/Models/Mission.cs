using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class Mission
    {
        public enum MissionStatus
        {
            Unassigned,
            Incomplete,
            Complete
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MissionStatus Status { get; set; }
        public string StatusDetail { get; set; }
        public int ExperiencePoints { get; set; }
        public int AttackLevelBoost { get; set; }

        public Mission() { } // default constructor

        public Mission(int id, string name, MissionStatus status)
        {
            Id = id;
            Name = name;
            Status = status;
        }
    }
}
