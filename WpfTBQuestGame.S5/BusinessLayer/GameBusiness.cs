using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.PresentationLayer;
using TBQuestGame.Models;
using TBQuestGame.DataLayer;


namespace TBQuestGame.BusinessLayer
{
    public class GameBusiness
    {
        GameSessionViewModel _gameSessionViewModel;
        
        Player _player = new Player();
        PlayerEntryAndSetupView _playerEntryAndSetupView;
        Map _masterGameMap;
        ButtonVisibility _buttonVisibility = new ButtonVisibility();
       
        
        public GameBusiness()
        {
            LoadGameData();
            ShowGameView();
        }

        private void LoadGameData()
        {
            _playerEntryAndSetupView = new PlayerEntryAndSetupView(_player);
            _playerEntryAndSetupView.ShowDialog();

            if (_player.NewPlayer)
            {
                //load low health player for new game 
                _player = GameData.DefaultPlayer(_player.NewPlayer, _player.Name);
                
            }
            else
            {
                //this is where gamedata will load returning player details
                _player = GameData.DefaultPlayer();
                
            }
            _masterGameMap = GameData.MasterGameMap();
            _buttonVisibility = GameData.GameStartButtonView();

        }



        private void ShowGameView()
        {
            _gameSessionViewModel = new GameSessionViewModel(_player, _masterGameMap, _buttonVisibility);

            GameSessionView gameSessionView = new GameSessionView(_gameSessionViewModel);

            gameSessionView.DataContext = _gameSessionViewModel;
            gameSessionView.Show();
            _playerEntryAndSetupView.Close();
        }

    }

}
