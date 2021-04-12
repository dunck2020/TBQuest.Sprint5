using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TBQuestGame.PresentationLayer;

namespace TBQuestGame.PresentationLayer
{
    /// <summary>
    /// Interaction logic for GameSessionView.xaml
    /// </summary>
    public partial class GameSessionView : Window
    {
        GameSessionViewModel _gameSessionViewModel;

        public GameSessionView(GameSessionViewModel gameSessionViewModel)
        {
            _gameSessionViewModel = gameSessionViewModel;
            InitializeComponent();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MapButton_Click(object sender, RoutedEventArgs e)
        {
            Button mapButton = sender as Button;
            int.TryParse(mapButton.Tag.ToString(), out int areaID);
            _gameSessionViewModel.PlayerAdvance(areaID);  
        }

        private void PickUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (LocationItemsDataGrid.SelectedItem != null)
            {
                _gameSessionViewModel.AddItemToInventory();
            }
        }

        private void PutDownButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerDataTabControl.SelectedItem != null)
            {
                _gameSessionViewModel.RemoveItemFromInventory();
            }
        }

        private void UseButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerDataTabControl.SelectedItem != null)
            {
               _gameSessionViewModel.OnUseGameItem();
            }
        }

        private void SpeakTo_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (LocationNpcsDataGrid.SelectedItem != null)
            {
                _gameSessionViewModel.OnPlayerTalkTo();

            }
        }

        private void AttackButton_Click(object sender, RoutedEventArgs e)
        {
            if (LocationNpcsDataGrid.SelectedItem != null)
            {
                _gameSessionViewModel.OnPlayerAttack();
            }
        }

        private void DefendButton_Click(object sender, RoutedEventArgs e)
        {
            if (LocationNpcsDataGrid.SelectedItem != null)
            {
                _gameSessionViewModel.OnPlayerDefend();
            }
        }

        private void RetreatButton_Click(object sender, RoutedEventArgs e)
        {
            if (LocationNpcsDataGrid.SelectedItem != null)
            {
                _gameSessionViewModel.OnPlayerRetreat();
            }
        }

        private void MissionStatus_Click(object sender, RoutedEventArgs e)
        {
            _gameSessionViewModel.OpenMissionStatusView();
        }
    }
}
