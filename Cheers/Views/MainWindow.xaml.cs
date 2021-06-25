using System;
using System.Collections.Generic;
using System.Windows;

namespace Chees
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChangeStartWindowVisibility(bool visibility)
        {
            if (visibility)
            {
                Chees.Visibility = Visibility.Visible;
            }
            else
            {
                Chees.Visibility = Visibility.Hidden;
            }
        }

        private void ChangeGameWindowVisibility(bool visibility)
        {
            if (visibility)
            {
                Game.Visibility = Visibility.Visible;
            }
            else
            {
                Game.Visibility = Visibility.Hidden;
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            Game.Visibility = Visibility.Visible;
            Chees.Visibility = Visibility.Hidden;
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            ChangeNames.Visibility = Visibility.Visible;
            Chees.Visibility = Visibility.Hidden;
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            ChangeNames.Visibility = Visibility.Hidden;
            Game.Visibility = Visibility.Visible;;
        }
 

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            ChangeGameWindowVisibility(false);
            ChangeStartWindowVisibility(true);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ChangeStartWindowVisibility(true);
        }
    }
}
