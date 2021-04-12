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
using TBQuestGame.Models;
using TBQuestGame.DataLayer;

namespace TBQuestGame.PresentationLayer
{
    /// <summary>
    /// Interaction logic for PlayerEntryAndSetupView.xaml
    /// </summary>
    /// 
    public partial class PlayerEntryAndSetupView : Window
    {
        private Player _player;

        public PlayerEntryAndSetupView(Player player)
        {
            _player = player;
            InitializeComponent();
        }


        private void PlayerSetupButton_Click(object sender, RoutedEventArgs e)
        {
            Button setUpButton = sender as Button;
            if (UserName.Text != "")
            {
                switch (setUpButton.Name)
                {
                    case "newPlayer":
                        _player.NewPlayer = true;
                        _player.Name = UserName.Text;
                        Visibility = Visibility.Hidden;
                        break;

                    case "loadPlayer":
                        _player.NewPlayer = false;
                        //use text to verify username to load data in gamebusiness
                        Visibility = Visibility.Hidden;
                        break;
                }
            }
            else
            {
                MessageBox.Show("Username is required to continue.", "Input needed!");
            }
        }
    }
}
