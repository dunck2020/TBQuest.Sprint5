using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.PresentationLayer;

namespace TBQuestGame.Models
{
    public class Location
    {
        #region FIELDS

        private int _id;
        private string _name;
        private string _description;
        private bool _isAccessible;
        private string _locationMessage;
        private int _modifyLives;
        private int _modifyHealth;
        private List<int> _currentAvailableLocations;
        private ObservableCollection<GameItem> _gameItems;
        private ObservableCollection<Npc> _npcs;




        #endregion

        #region PROPERTIES

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public bool IsAccessible
        {
            get { return _isAccessible; }
            set { _isAccessible = value; }
        }
        public string LocationMessage
        {
            get 
            {   
                return _locationMessage; 
            }
            set 
            { 
                _locationMessage = value; 
            }
        }
        public int ModifyLives
        {
            get { return _modifyLives; }
            set { _modifyLives = value; }
        }
        public int ModifyHealth
        {
            get { return _modifyHealth; }
            set { _modifyHealth = value; }
        }
        public List<int> CurrentAvailableLocations
        {
            get{ return _currentAvailableLocations; }
            set { _currentAvailableLocations = value; }
        }
        public ObservableCollection<GameItem> GameItems
        {
            get { return _gameItems; }
            set { _gameItems = value; }
        }
        public ObservableCollection<Npc> Npcs
        {
            get { return _npcs; }
            set { _npcs = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Location()
        {
            _gameItems = new ObservableCollection<GameItem>();
        }

        #endregion

        #region METHODS

        public void UpdateLocationGameItems()
        {
            ObservableCollection<GameItem> updatedLocationGameItems = new ObservableCollection<GameItem>();

            foreach (GameItem gameItem in _gameItems)
            {
                updatedLocationGameItems.Add(gameItem);
            }

            GameItems.Clear();

            foreach (GameItem gameItem in updatedLocationGameItems)
            {
                GameItems.Add(gameItem);
            }
        }
        public void AddGameItemToLocation(GameItem selectedGameItem)
        {
            if (selectedGameItem != null)
            {
                _gameItems.Add(selectedGameItem);
            }

            UpdateLocationGameItems();
        }
        public void RemoveGameItemFromLocation(GameItem selectedGameItem)
        {
            if (selectedGameItem != null)
            {
                _gameItems.Remove(selectedGameItem);
            }

            UpdateLocationGameItems();
        }

        #endregion
    }
}
