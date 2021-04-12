using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TBQuestGame.DataLayer;
using TBQuestGame.BusinessLayer;
using TBQuestGame.Models;
using System.Windows;
using System.Diagnostics;

namespace TBQuestGame.PresentationLayer
{
    public class GameSessionViewModel : ObservableObject
    {
        //random object
        private Random random = new Random();

        #region CONSTANTS

        const string TAB = "\t";
        const string NEW_LINE = "\n";

        #endregion

        #region FIELDS

        private Player _player;
        private string _gameMessage;
        private Map _masterGameMap;
        private Location _currentLocation;
        private string _currentLocationInformation;
        private ObservableCollection<Location> _accessibleLocations;
        private string _currentLocationName;
        private GameItem _currentGameItem;
        private ButtonVisibility _buttonVisibility;
        private Npc _currentNpc;

        #endregion

        #region PROPERTIES

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }
        public string GameMessage
        {
            set 
            { 
                _gameMessage = value;
                OnPropertyChanged(nameof(GameMessage));
            }
            get
            {
                if (Player.NewPlayer)
                {
                    _gameMessage = GameData.DEFAULT_GAME_MESSAGE + "\n\n\n" + CurrentLocation.LocationMessage.ToString();
                }
                else
                {
                    _gameMessage = CurrentLocation.LocationMessage.ToString();
                }
                return _gameMessage; 
            }
        }
        public Map MasterGameMap
        {
            get { return _masterGameMap; }
            set { _masterGameMap = value; }
        }
        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                _currentLocationInformation = _currentLocation.Description;
                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(CurrentLocationInformation));
            }       
        }
        public string CurrentLocationInformation
        {
            get { return _currentLocationInformation; }
            set
            {
                _currentLocationInformation = value;
                OnPropertyChanged(nameof(CurrentLocationInformation));
            }
        }
        public ObservableCollection<Location> AccessibleLocations
        {
            get { return _accessibleLocations; }
            set
            {
                _accessibleLocations = value;
                OnPropertyChanged(nameof(AccessibleLocations));
            }
        }
        public string CurrentLocationName
        {
            get { return _currentLocationName; }
            set 
            {
                _currentLocationName = value;
                OnPropertyChanged(nameof(CurrentLocationName));
            }
        }
        public GameItem CurrentGameItem
        {
            get { return _currentGameItem; }
            set { _currentGameItem = value; }
        }
        public ButtonVisibility ButtonVisibility
        {
            get { return _buttonVisibility; }
            set { _buttonVisibility = value; }
        }
        public Npc CurrentNpc
        {
            get { return _currentNpc; }
            set { _currentNpc = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public GameSessionViewModel() {} //Default Constructor
        public GameSessionViewModel(Player player, Map masterGameMap, ButtonVisibility buttonVisibility)
        {
            _player = player;
            _masterGameMap = masterGameMap;
            _buttonVisibility = buttonVisibility;
            
            InitializeView();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// game start up methods
        /// </summary>
        private void InitializeView()
        {
            CurrentLocation = MasterGameMap.CurrentLocation;
            AccessibleLocations = new ObservableCollection<Location>();
            UpdateAccessibleLocations();
            UpdateVisibleButtons();
            Player.InventoryUpdate();
            Player.CalculateWealth();
            UpdateAccessibleGameItems();
        }

        #region MISSION INTERACTION

        private string GenerateMissionGatherDetail(MissionGather mission)
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();

            sb.AppendLine("All Required Game Items");
            foreach (var gameItem in mission.RequiredGameItems)
            {
                sb.AppendLine(TAB + gameItem.Name);
            }

            if (mission.Status == Mission.MissionStatus.Incomplete)
            {
                sb.AppendLine("Game Items to Collect");
                foreach (var gameItem in mission.GameItemsNotCompleted(Player.Inventory.ToList()))
                {
                    sb.AppendLine(TAB + gameItem.Name);
                }
            }

            sb.Remove(sb.Length - 2, 2); // remove the last two characters that generate a blank line

            return sb.ToString(); ;
        }

        private string GenerateMissionTravelDetail(MissionTravel mission)
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();

            sb.AppendLine("All Required Areas to Visit");
            foreach (var location in mission.RequiredLocations)
            {
                sb.AppendLine(TAB + location.Name);
            }

            if (mission.Status == Mission.MissionStatus.Incomplete)
            {
                sb.AppendLine("Locations you still need");
                foreach (var location in mission.LocationsNotCompleted(Player.LocationsVisited))
                {
                    sb.AppendLine(TAB + location.Name);
                }
            }
            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }

        private string GenerateMissionEngageDetail(MissionEngage mission)
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();

            sb.AppendLine("All Required People or Creatures");
            foreach (var location in mission.RequiredNpcs)
            {
                sb.AppendLine(TAB + location.Name);
            }

            if (mission.Status == Mission.MissionStatus.Incomplete)
            {
                sb.AppendLine("Meet these now");
                foreach (var location in mission.NpcsNotCompleted(Player.NpcsEngaged))
                {
                    sb.AppendLine(TAB + location.Name);
                }
            }
            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }

        private string GenerateMissionStatusInformation()
        {
            string missionStatusInformation;

            double totalMissions = Player.Missions.Count();
            double missionsCompleted = Player.Missions.Where(m => m.Status == Mission.MissionStatus.Complete).Count();

            int percentMissionsCompleted = (int)((missionsCompleted / totalMissions) * 100);
            missionStatusInformation = $"Missions Complete: {percentMissionsCompleted}%" + NEW_LINE;

            if (percentMissionsCompleted == 0)
            {
                missionStatusInformation += "Get to work, time is limited.";
            }
            else if (percentMissionsCompleted <= 33)
            {
                missionStatusInformation += "Long road ahead but you have made some progress.";
            }
            else if (percentMissionsCompleted <= 66)
            {
                missionStatusInformation += "You have made significant progress, continue on the great path.";
            }
            else if (percentMissionsCompleted == 100)
            {
                missionStatusInformation += "Good job, missions have been completed.";
            }

            return missionStatusInformation;
        }

        private MissionStatusViewModel InitializeMissionStatusViewModel()
        {
            MissionStatusViewModel missionStatusViewModel = new MissionStatusViewModel();

            missionStatusViewModel.MissionInformation = GenerateMissionStatusInformation();

            missionStatusViewModel.Missions = new List<Mission>(Player.Missions);
            foreach (Mission mission in missionStatusViewModel.Missions)
            {
                if (mission is MissionTravel)
                    mission.StatusDetail = GenerateMissionTravelDetail((MissionTravel)mission);

                if (mission is MissionEngage)
                    mission.StatusDetail = GenerateMissionEngageDetail((MissionEngage)mission);

                if (mission is MissionGather)
                    mission.StatusDetail = GenerateMissionGatherDetail((MissionGather)mission);
            }

            return missionStatusViewModel;
        }

        public void OpenMissionStatusView()
        {
            MissionStatusView missionStatusView = new MissionStatusView(InitializeMissionStatusViewModel());

            missionStatusView.Show();
        }

        #endregion

        #region NPC INTERACTION

        public void OnPlayerTalkTo()
        {
            if (CurrentNpc != null && CurrentNpc is ISpeak)
            {
                ISpeak speakingNpc = CurrentNpc as ISpeak;
                CurrentLocationInformation = speakingNpc.Speak();
                Player.NpcsEngaged.Add(CurrentNpc);
                Player.UpdateMissionStatus();
                if (CurrentNpc.Id == 1003 && CurrentNpc.Inventory.Count != 0)
                {
                    GameItem gameItem = CurrentNpc.Inventory[0];
                    CurrentLocation.AddGameItemToLocation(gameItem);
                    CurrentNpc.RemoveGameItemFromNpc(gameItem);
                }
            }
            else
            {
                CurrentLocationInformation = "Beasts can not speak!";
            }
        }

        public void OnPlayerAttack()
        {
            if (CurrentNpc != null) Player.NpcsEngaged.Add(CurrentNpc);
            Player.BattleMode = BattleModeName.ATTACK;
            Battle();
            Player.UpdateMissionStatus();
        }

        public void OnPlayerDefend()
        {
            Player.BattleMode = BattleModeName.DEFEND;
            Battle();
            if (CurrentNpc != null)
                Player.NpcsEngaged.Add(CurrentNpc);
            Player.UpdateMissionStatus();
        }

        public void OnPlayerRetreat()
        {
            Player.BattleMode = BattleModeName.RETREAT;
            Battle();
            if (CurrentNpc != null)
                Player.NpcsEngaged.Add(CurrentNpc);
            Player.UpdateMissionStatus();
        }

        private void Battle()
        {
            if (CurrentNpc is IBattle)
            {
                IBattle battleNpc = CurrentNpc as IBattle;
                int playerHitPoints;
                int battleNpcHitPoints = 0;
                string battleInformation = "";

                playerHitPoints = CalculatePlayerHitPoints();
                if (battleNpc.CurrentWeapon != GameData.GameItemById(1000) as Weapon)
                {
                    battleNpcHitPoints = CalculateNpcHitPoints(battleNpc);
                }
                else
                {
                    battleInformation = "It appears you are attacking an unarmed assailant" + Environment.NewLine;                
                }
                battleInformation +=
                    $"Player: {Player.BattleMode}        Hit Points:  {playerHitPoints} " + Environment.NewLine +
                    $"NPC: {battleNpc.BattleMode}        Hit Points:  {battleNpcHitPoints} " + Environment.NewLine;

                if (playerHitPoints >= battleNpcHitPoints)
                {
                    battleInformation += $"You have slain {CurrentNpc.Name}" + Environment.NewLine +
                        "Check the items tab, the slain NPC may have dropped something";

                    //defeated Npc drops items
                    if (CurrentNpc.Inventory != null)
                    {
                        foreach (var gameItem in CurrentNpc.Inventory)
                        {
                            _currentLocation.AddGameItemToLocation(gameItem);
                        }
                    }
                    _currentLocation.Npcs.Remove(CurrentNpc);
                }
                else
                {
                    battleInformation += $"You have been slain by {_currentNpc.Name}";
                    Player.Lives--;
                    OnPlayerDies("You have been slain.");
                }
                CurrentLocationInformation = battleInformation;
                if (Player.Lives <= 0) OnPlayerDies("You have been slain.");
            }
            else
            {
                CurrentLocationInformation = "The current NPC is not battle ready";
            }

        }

        private int CalculatePlayerHitPoints()
        {
            int playerHitPoints = 0;
            switch (Player.BattleMode)
            {
                case BattleModeName.ATTACK:
                    playerHitPoints = Player.Attack();
                    break;
                case BattleModeName.DEFEND:
                    playerHitPoints = Player.Defend();
                    break;
                case BattleModeName.RETREAT:
                    playerHitPoints = Player.Retreat();
                    break;
            }
            return playerHitPoints;
        }

        private int CalculateNpcHitPoints(IBattle battleNpc)
        {
            int npcHitPoints = 0;
            switch (NpcBattleResponse())
            {
                case BattleModeName.ATTACK:
                    npcHitPoints = battleNpc.Attack();
                    break;
                case BattleModeName.DEFEND:
                    npcHitPoints = battleNpc.Defend();
                    break;
                case BattleModeName.RETREAT:
                    npcHitPoints = battleNpc.Retreat();
                    break;
            }
            return npcHitPoints;
        }

        private BattleModeName NpcBattleResponse()
        {
            BattleModeName npcBattleResponse = BattleModeName.RETREAT;

            switch (DieRoll(3))
            {
                case 1:
                    npcBattleResponse = BattleModeName.ATTACK;
                    break;
                case 2:
                    npcBattleResponse = BattleModeName.DEFEND;
                    break;
                case 3:
                    npcBattleResponse = BattleModeName.RETREAT;
                    break;
            }
            return npcBattleResponse;
        }

        #endregion

        #region PLAYER MOVEMENT

        /// <summary>
        /// fires when a player clicks on a map area button
        /// </summary>
        /// <param name="areaID"></param>
        public void PlayerAdvance(int areaID)
        {
            Player.NewPlayer = false;

            foreach (Location location in MasterGameMap.Locations)
            {
                if (areaID == location.Id)
                {
                    CurrentLocation = location;
                    GameMessage = CurrentLocation.LocationMessage;
                    Player.LocationsVisited.Add(location);
                    Player.UpdateMissionStatus();
                }
            }

            ModifyPlayerHealth();
            ModifyPlayerLives();
            UpdateAccessibleLocations();
            UpdateVisibleButtons();
        }

        /// <summary>
        /// updates areas that are accessible from current area
        /// </summary>
        private void UpdateAccessibleLocations()
        {
            //clear accessible locations
            AccessibleLocations.Clear();

            //start with no accessible locations
            foreach (Location location in MasterGameMap.Locations)
            {
                location.IsAccessible = false;
            }


            //update available locations based on current location
            foreach (int locationId in CurrentLocation.CurrentAvailableLocations)
            {
                foreach (Location location in _masterGameMap.Locations)
                {
                    if (location.Id == locationId)
                    {
                        location.IsAccessible = true;
                        _accessibleLocations.Add(location);
                    }
                }
            }
        }

        /// <summary>
        /// only areas that area accessible will show their buttons
        /// </summary>
        private void UpdateVisibleButtons()
        {
            foreach (Location location in MasterGameMap.Locations)
            {
                ButtonVisibility.ButtonUpdate(location);
            }
        }

        #endregion

        #region INVENTORY CONTROL

        /// <summary>
        /// updates items that area available in certain areas
        /// </summary>
        private void UpdateAccessibleGameItems()
        {
            foreach (Location location in MasterGameMap.Locations)
            {
                for (int i = 0; i < location.GameItems.Count; i++)
                {
                    GameItem gameItem = location.GameItems[i];
                    if (!gameItem.IsAvailable)
                    {
                        location.RemoveGameItemFromLocation(gameItem);
                    }
                }           
            }
        }

        /// <summary>
        /// removes game item from location and adds to player inventory
        /// </summary>
        public void AddItemToInventory()
        {

            if (CurrentGameItem != null && CurrentLocation.GameItems.Contains(_currentGameItem))
            {
                GameItem selectedGameItem = CurrentGameItem as GameItem;

                //removes from location and adds to player inventory
                CurrentLocation.RemoveGameItemFromLocation(selectedGameItem);
                Player.AddGameItemToInventory(selectedGameItem);

                OnPlayerPickUp(selectedGameItem);
            }
        }

        /// <summary>
        /// removes inventory from player and adds to location
        /// </summary>
        public void RemoveItemFromInventory()
        {
            if (CurrentGameItem != null)
            {
                GameItem selectedGameItem = CurrentGameItem as GameItem;
                CurrentLocation.AddGameItemToLocation(selectedGameItem);
                Player.RemoveGameItemFromInventory(selectedGameItem);
                OnPlayerPutDown(selectedGameItem);
            }
        }

        /// <summary>
        /// adds any gameitem value to player
        /// </summary>
        /// <param name="gameItem"></param>
        private void OnPlayerPickUp(GameItem gameItem)
        {
            Player.Wealth += gameItem.Value;
            Player.UpdateMissionStatus();
        }

        /// <summary>
        /// removes value of game item and if currently selected weapon removes attack points
        /// </summary>
        /// <param name="gameItem"></param>
        private void OnPlayerPutDown(GameItem gameItem)
        {
            if (gameItem is Weapon weapon)
            {
                if (Player.AttackPoints == weapon.AttackPoints)
                {
                    //return to default weapon
                    Player.CurrentWeapon = GameData.GameItemById(1000) as Weapon;
                    Player.AttackPoints = Player.CurrentWeapon.AttackPoints;
                }
            }
            Player.UpdateMissionStatus();
            Player.Wealth -= gameItem.Value;
        }

        /// <summary>
        /// switch statement to process different game items
        /// </summary>
        public void OnUseGameItem()
        {
            switch (_currentGameItem)
            {
                case Spell spell:
                    ProcessSpellUse(spell);
                    break;
                case Artifact artifact:
                    ProcessArtifacUse(artifact);
                    break;
                case Weapon weapon:
                    ProcessWeaponUse(weapon);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// using a weapon 'arms' the player with the weapon
        /// </summary>
        /// <param name="weapon"></param>
        private void ProcessWeaponUse(Weapon weapon)
        {
            Player.CurrentWeapon = weapon;
            Player.AttackPoints = weapon.AttackPoints;
            Player.UpdateMissionStatus();
        }

        /// <summary>
        /// based on artifact use action switch statement to process different use actions
        /// </summary>
        /// <param name="artifact"></param>
        private void ProcessArtifacUse(Artifact artifact)
        {
            switch (artifact.UseAction)
            {
                case Artifact.UseActionType.KILL_PLAYER:
                    Player.Lives--;
                    OnPlayerDies(artifact.UseMessage);
                    Player.Wealth = Player.Wealth - artifact.Value;
                    Player.RemoveGameItemFromInventory(_currentGameItem);
                    break;
                case Artifact.UseActionType.OPEN_LOCATION:
                    PlayerAdvance(100);
                    Player.Wealth = Player.Wealth - artifact.Value;
                    Player.RemoveGameItemFromInventory(_currentGameItem);
                    break;
                case Artifact.UseActionType.HEAL_PLAYER:
                    Player.Lives = 3;
                    Player.Health = 100;
                    Player.Wealth = Player.Wealth - artifact.Value;
                    Player.RemoveGameItemFromInventory(_currentGameItem);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// based on spell use action switch statement to process different use actions
        /// </summary>
        /// <param name="spell"></param>
        private void ProcessSpellUse(Spell spell)
        {
            Player.Health += spell.HealthChange;
            Player.Lives += spell.LivesChange;
            Player.RemoveGameItemFromInventory(_currentGameItem);
        }

        #endregion

        #region PLAYER HEALTH

        /// <summary>
        /// areas that modify player lives use this method when movement occurs
        /// </summary>
        private void ModifyPlayerLives()
        {
            if (CurrentLocation.ModifyLives != 0)
            {
                Player.Lives += CurrentLocation.ModifyLives;
                OnPlayerDies();
            }
        }

        /// <summary>
        /// areas that modify player health use this method when movement occurs
        /// </summary>
        private void ModifyPlayerHealth()
        {
            int healthAdjustment = CurrentLocation.ModifyHealth;
            if (healthAdjustment != 0)
            {
                if (-healthAdjustment >= Player.Health)
                {
                    Player.Health += healthAdjustment;
                    Player.Lives--;
                    OnPlayerDies();
                }
                else
                {
                    Player.Health += healthAdjustment;
                }
                
            }
        }

        /// <summary>
        /// process message box when player dies
        /// </summary>
        /// <param name="message"></param>
        public void OnPlayerDies(string message = "")
        {
            if (Player.Lives == 0)
            {
                string messagetext = message + $"\n\nYOU DIED! You have {Player.Lives} lives remaining. \n\n Play Again?";

                string titleText = "Death";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show(messagetext, titleText, button);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                        break;
                    case MessageBoxResult.No:
                        Environment.Exit(0);
                        break;
                }
            }
            else
            {
                string messageText = message + $"\n\nYOU DIED! You have {Player.Lives} lives remaining.  You will start in the Village!";
                string titleText = "Death";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBox.Show(messageText, titleText, button);
                PlayerAdvance(100);
            }
        }

        #endregion

        #region HELPER METHODS

        private int DieRoll(int sides)
        {
            return random.Next(1, sides + 1);
        }

        #endregion

        #endregion

    }
}
