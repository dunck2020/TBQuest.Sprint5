using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.PresentationLayer;

namespace TBQuestGame.Models
{
    public class Player : Character, IBattle
    {
        #region ENUMS

        #endregion

        #region FIELDS

        private bool _newPlayer;
        private int _lives;
        private int _attackPoints;
        private int _health;
        private int _wealth;
        private BattleModeName _battleMode;
        private Weapon _currentWeapon;
        private int _attackLevel;
        private List<Location> _locationsVisited;
        private List<Npc> _npcsEngaged;
        private ObservableCollection<GameItem> _inventory;
        private ObservableCollection<GameItem> _artifacts;
        private ObservableCollection<GameItem> _weapons;
        private ObservableCollection<GameItem> _spells;
        private ObservableCollection<GameItem> _treasure;
        private ObservableCollection<Mission> _missions;    
        
        #endregion

        #region PROPERTIES

        public bool NewPlayer
        {
            get { return _newPlayer; }
            set { _newPlayer = value; }
        }
        public int Lives
        {
            get { return _lives; }
            set 
            {    
                _lives = value;
                OnPropertyChanged(nameof(Lives));   
            }
        }
        public int AttackPoints
        {
            get 
            { 
                return _attackPoints; 
            }
            set 
            { 
                _attackPoints = value;
                OnPropertyChanged(nameof(AttackPoints));
            }
        }
        public int Health
        {
            get { return _health; }
            set
            {
                _health = value;

                if (_health > 100)
                {
                    _health = 100;
                    
                }
                else if (_health <= 0)
                {
                    _health = 100;             
                }

                OnPropertyChanged(nameof(Health));
            }
        }
        public int Wealth
        {
            get { return _wealth; }
            set
            {
                _wealth = value;
                OnPropertyChanged(nameof(Wealth));
            }
        }
        public BattleModeName BattleMode
        {
            get { return _battleMode; }
            set { _battleMode = value; }
        }
        public Weapon CurrentWeapon
        {
            get { return _currentWeapon; }
            set { _currentWeapon = value; }
        }
        public int AttackLevel
        {
            get { return _attackLevel; }
            set { _attackLevel = value; }
        }
        public List<Location> LocationsVisited
        {
            get { return _locationsVisited; }
            set { _locationsVisited = value; }
        }        
        public List<Npc> NpcsEngaged
        {
            get { return _npcsEngaged; }
            set { _npcsEngaged = value; }
        }
        public ObservableCollection<GameItem> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }
        public ObservableCollection<GameItem> Artifacts
        {
            get { return _artifacts; }
            set { _artifacts = value; }
        }
        public ObservableCollection<GameItem> Weapons
        {
            get { return _weapons; }
            set { _weapons = value; }
        }
        public ObservableCollection<GameItem> Spells
        {
            get { return _spells; }
            set { _spells = value; }
        }        
        public ObservableCollection<GameItem> Treasure
        {
            get { return _treasure; }
            set { _treasure = value; }
        }
        public ObservableCollection<Mission> Missions
        {
            get { return _missions; }
            set { _missions = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Player()
        {
            _locationsVisited = new List<Location>();
            _npcsEngaged = new List<Npc>();
            _weapons = new ObservableCollection<GameItem>();
            _treasure = new ObservableCollection<GameItem>();
            _artifacts = new ObservableCollection<GameItem>();
            _spells = new ObservableCollection<GameItem>();
            _missions = new ObservableCollection<Mission>();
        }

        public Player(  int id, string name, int location, Genus kind, 
                        int lives, int attackLevel, int health, bool newPLayer, Weapon currentWeapon)
            : base(id, name, location, kind)
        {
            Lives = lives;
            AttackLevel = attackLevel;
            Health= health;
            NewPlayer = newPLayer;
            CurrentWeapon = currentWeapon;
        }

        #endregion

        #region METHODS

        public void CalculateWealth()
        {
            Wealth = _inventory.Sum(i => i.Value);
        }
        public void InventoryUpdate()
        {
            Weapons.Clear();
            Artifacts.Clear();
            Spells.Clear();
            Treasure.Clear();


            foreach (var gameItem in _inventory)
            {
                if (gameItem is Weapon) Weapons.Add(gameItem); //1000
                if (gameItem is Artifact) Artifacts.Add(gameItem); //2000
                if (gameItem is Spell) Spells.Add(gameItem); //3000
                if (gameItem is Treasure) Treasure.Add(gameItem); //4000
                
            }
        }
        public void AddGameItemToInventory(GameItem selectedGameItem)
        {
            if (selectedGameItem != null)
            {
                _inventory.Add(selectedGameItem);
            }
            InventoryUpdate();
        }
        public void RemoveGameItemFromInventory(GameItem selectedGameItem)
        {
            if (selectedGameItem != null)
            {
                _inventory.Remove(selectedGameItem);
            }
            InventoryUpdate();
        }
        public int Attack()
        {
            int attackpoints = CurrentWeapon.AttackPoints * AttackLevel;
            return attackpoints;
        }
        public int Defend()
        {
            int attackpoints = CurrentWeapon.AttackPoints * AttackLevel;
            return attackpoints;
        }
        public int Retreat()
        {
            int attackpoints = CurrentWeapon.AttackPoints * AttackLevel;
            return attackpoints;
        }
        public void UpdateMissionStatus()
        {
            foreach (Mission mission in _missions.Where(m => m.Status == Mission.MissionStatus.Incomplete))
            {
                if (mission is MissionTravel)
                {
                    if (((MissionTravel)mission).LocationsNotCompleted(LocationsVisited).Count == 0)
                    {
                        mission.Status = Mission.MissionStatus.Complete;
                        AttackLevel += mission.ExperiencePoints;
                    }
                }
                else if (mission is MissionEngage)
                {
                    if (((MissionEngage)mission).NpcsNotCompleted(NpcsEngaged).Count == 0)
                    {
                        mission.Status = Mission.MissionStatus.Complete;
                        AttackLevel += mission.ExperiencePoints;
                    }
                }
                else if (mission is MissionGather)
                {
                    if (((MissionGather)mission).GameItemsNotCompleted(_inventory.ToList()).Count == 0)
                    {
                        mission.Status = Mission.MissionStatus.Complete;
                        AttackLevel += mission.ExperiencePoints;
                    }
                }
                else
                {
                    throw new Exception("Unknown Mission child class.");
                }
            }
        }

        #endregion


    }
}
