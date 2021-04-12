using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TBQuestGame.Models
{
    public class Map
    {
        #region FIELDS

        private List<Location> _locations;
        private Location _currentLocation;
        private List<GameItem> _standardGameItems;

        #endregion

        #region PROPERTIES

        public List<Location> Locations
        {
            get { return _locations; }
            set { _locations = value; }
        }
        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set { _currentLocation = value; }
        }
        public List<GameItem> StandardGameItems
        {
            get { return _standardGameItems; }
            set { _standardGameItems = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public Map()
        {
            _locations = new List<Location>();
        }

        #endregion

        #region METHODS

        public string OpenLocationsByArtifact(int artifactID)
        {
            ///NEED TO CODE OUT
            ///HOME WILL HAVE A REQUIRED RELIC
            ///CAVE WILL HAVE A REQUIRED RELIC
            string message = "The relic did nothing.";
            Location mapLocation = new Location();
            if (true)
            {
                mapLocation.IsAccessible = true;
                message = $"{mapLocation.Name} is now accessible.";
            }
;
            return message;
        }

        #endregion

    }
}
