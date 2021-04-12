using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class MissionTravel : Mission
    {
        private int _id;
        private string _name;
        private string _description;
        private MissionStatus _status;
        private string _statusDetail;
        private int _experiencePoints;
        public List<Location> RequiredLocations { get; set; }

        public MissionTravel() { } // default

        public  MissionTravel(int id, string name, MissionStatus status, List<Location> requiredLocations)
            :base(id, name, status)
        {
            _id = id;
            _name = name;
            _status = status;
            RequiredLocations = requiredLocations;
        }

        public List<Location> LocationsNotCompleted(List<Location> locationsTraveled)
        {
            List<Location> locationsToComplete = new List<Location>();

            foreach (var requiredLocation in RequiredLocations)
            {
                if (!locationsTraveled.Any(l => l.Id == requiredLocation.Id))
                {
                    locationsToComplete.Add(requiredLocation);
                }
            }
            return locationsToComplete;
        }
    }
}
