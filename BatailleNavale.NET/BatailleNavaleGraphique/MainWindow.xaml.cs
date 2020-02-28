using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BatailleNavaleGraphique
{
    public partial class MainWindow : Window
    {

                public MainWindow()
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Jeu NouvellePartie = new Jeu();
            NouvellePartie.Show();
        }


        void NewGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void Options_Click(object sender, RoutedEventArgs e)
        {
            Options Option = new Options();
            Option.Show();
        }

        void Charger_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer mp = new MediaPlayer();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                mp.Open(new Uri(openFileDialog.FileName));
                mp.Play();
            }
        }

        
}
}
